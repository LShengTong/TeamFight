using UnityEngine;

namespace Data
{
    public class PlayerHero : Hero
    {
        public SlotIndex slotIndex;

        public void Initialize(HeroType inHeroType, SlotIndex inSlotIndex) 
        {
            Initialize(inHeroType);
            slotIndex = inSlotIndex;
        }
    }
}