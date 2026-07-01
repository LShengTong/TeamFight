using Data;
using UnityEngine;

namespace Service
{
    public class HeroPlacementService: MonoBehaviour
    {
        [SerializeField] private Level level;
        [SerializeField] private Heroes heroes;
        
        public bool TryPlace(int sourceHeroId, SlotIndex targetSlot)
        {
            var targetHeroId = heroes.GetHeroIdBySlotIndex(targetSlot);
            if (targetHeroId != null)
            {
                return TryExchange(sourceHeroId, targetHeroId.Value);
            }

            var sourceHero = heroes.GetHero(sourceHeroId);
            if (!sourceHero) return false;
            if (!CanMoveToSlot(sourceHero, targetSlot)) return false;
            heroes.SetSlotIndex(sourceHeroId, targetSlot);
            return true;
        }

        private bool TryExchange(int sourceHeroId, int targetHeroId)
        {
            var sourceHero = heroes.GetHero(sourceHeroId);
            var targetHero = heroes.GetHero(targetHeroId);
            if (!sourceHero || !targetHero) return false;
            heroes.SetSlotIndex(new []{ sourceHeroId, targetHeroId }, 
                new []{ targetHero.slotIndex, sourceHero.slotIndex });
            return true;
        }

        private bool CanMoveToSlot(PlayerHero sourceHero, SlotIndex targetSlot)
        {
            if (sourceHero.slotIndex.slotType == SlotType.Battle) return true;
            if (targetSlot.slotType != SlotType.Battle) return true;
            return heroes.GetHeroIdBySlotType(SlotType.Battle).Length < level.LevelValue;
        }
    }
}
