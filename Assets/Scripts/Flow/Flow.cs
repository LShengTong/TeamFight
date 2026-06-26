using System.Collections.Generic;
using UnityEngine;

namespace Flow
{
   public class Flow : MonoBehaviour
   {
      [SerializeField] private Warehouse warehouse;
      [SerializeField] private OptionalCards optionalCards;
      [SerializeField] private UIShop uiShop;
      [SerializeField] private Heroes heroesModel;
      [SerializeField] private EnemyHeroes enemyHeroesModel;
      [SerializeField] private HeroesView enemyHeroesView;
      [SerializeField] private BattleView enemyBattleView;
      [SerializeField] private Config.Config config;
   
      private void Start()
      {
         optionalCards.Set(warehouse.RandomTakeout(config.optionalCardCount));
         uiShop.OnCardClick += HandleCardClick;
         enemyHeroesModel.OnEnemyHeroInfoChange += HandleEnemyHeroesChange;
         HandleEnemyHeroesChange(enemyHeroesModel.Infos);
      }

      private void HandleCardClick(int index)
      {
         var cardType = optionalCards.Get(index);
         if (heroesModel.AddHeroToBench(cardType))
         {
            optionalCards.RemoveAt(index);
         }
      }

      private void HandleEnemyHeroesChange(Dictionary<int, EnemyHeroInfo> heroes)
      {
         var infos = new Dictionary<int, HeroType>();
         foreach (var pair in heroes)
         {
            infos[pair.Key] = pair.Value.HeroType;
         }
         enemyHeroesView.RefreshHeroes(infos);
         foreach (var pair in heroes)
         {
            var hero = enemyHeroesView.GetHero(pair.Key);
            if (!hero) return;
            hero.transform.position = enemyBattleView.GetSlotView(pair.Value.Row, pair.Value.Col)?.transform.position ?? Vector3.zero;
         }
      }
   }
}
