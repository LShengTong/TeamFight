using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Flow
{
    public class PlayerHeroesRefreshService : MonoBehaviour
    {
        [SerializeField] private Heroes heroesModel;
        [SerializeField] private HeroesView heroesView;
        [SerializeField] private BenchView benchView;
        [SerializeField] private BattleView battleView;

        private void Start()
        {
            heroesModel.OnHeroesChange += HandleHeroesChange;
            HandleHeroesChange(heroesModel.Infos);
        }
        
        private void HandleHeroesChange(Dictionary<int, PlayerHero> heroes)
        {
            var infos = new Dictionary<int, Hero>();
            foreach (var pair in heroes)
            {
                infos.Add(pair.Key, pair.Value);
            }
            heroesView.RefreshHeroes(infos);
            foreach (var pair in heroes)
            {
                var slotPosition = pair.Value.slotIndex.slotType switch
                {
                    SlotType.Battle => battleView.GetSlotView(pair.Value.slotIndex.x, pair.Value.slotIndex.y)?.transform.position ?? Vector3.zero,
                    SlotType.Bench => benchView.GetSlotView(pair.Value.slotIndex.x)?.transform.position ?? Vector3.zero,
                    _ => Vector3.zero
                };

                var hero = heroesView.GetHero(pair.Key);
                if (!hero) return;
                hero.transform.position = slotPosition;
            }
        }
    }
}