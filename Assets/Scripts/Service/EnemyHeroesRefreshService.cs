using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Service
{
    public class EnemyHeroesRefreshService : MonoBehaviour
    {
        [SerializeField] private EnemyHeroes heroesModel;
        [SerializeField] private HeroesView heroesView;
        [SerializeField] private BattleView battleView;

        private void Start()
        {
            heroesModel.OnEnemyHeroInfoChange += HandleHeroesChange;
            HandleHeroesChange(heroesModel.Heroes);
        }
        
        private void HandleHeroesChange(Dictionary<int, EnemyHero> heroes)
        {
            var infos = new Dictionary<int, Hero>();
            foreach (var pair in heroes)
            {
                infos.Add(pair.Key, pair.Value);
            }
            heroesView.RefreshHeroes(infos);
            foreach (var pair in heroes)
            {
                var hero = heroesView.GetHero(pair.Key);
                if (!hero) return;
                hero.transform.position = battleView.GetSlotView(pair.Value.row, pair.Value.col)?.transform.position ?? Vector3.zero;
            }
        }
    }
}