using System.Collections.Generic;
using UnityEngine;

namespace Flow
{
    public class PlayerSlotHero : MonoBehaviour
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

        public HeroView[] GetBattleHeroViews()
        {
            var heroViews = new List<HeroView>();
            foreach (var pair in heroesModel.Infos)
            {
                if (pair.Value.SlotIndex.slotType == SlotType.Battle)
                {
                    heroViews.Add(heroesView.GetHero(pair.Key));
                }
            }
            return heroViews.ToArray();
        }
        
        private void HandleHeroesChange(Dictionary<int, HeroInfo> heroes)
        {
            var infos = new Dictionary<int, HeroType>();
            foreach (var pair in heroes)
            {
                infos.Add(pair.Key, pair.Value.HeroType);
            }
            heroesView.RefreshHeroes(infos);
            foreach (var pair in heroes)
            {
                var slotPosition = pair.Value.SlotIndex.slotType switch
                {
                    SlotType.Battle => battleView.GetSlotView(pair.Value.SlotIndex.x, pair.Value.SlotIndex.y)?.transform.position ?? Vector3.zero,
                    SlotType.Bench => benchView.GetSlotView(pair.Value.SlotIndex.x)?.transform.position ?? Vector3.zero,
                    _ => Vector3.zero
                };

                var hero = heroesView.GetHero(pair.Key);
                if (!hero) return;
                hero.transform.position = slotPosition;
            }
        }
    }
}