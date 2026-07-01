using System;

namespace Config
{
    [Serializable]
    public struct WarehouseInfo
    {
        public HeroType heroType;
        public int cardCount;
    }

    [Serializable]

    public class WarehouseConfig
    {
        public WarehouseInfo[] infos =
        {
            new() { heroType = HeroType.Warrior, cardCount = 20 },
            new() { heroType = HeroType.Tank, cardCount = 20 },
            new() { heroType = HeroType.Archer, cardCount = 20 },
            new() { heroType = HeroType.Mage, cardCount = 20 }
        };
    }
}