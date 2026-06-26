using System;
using UnityEngine;

namespace Config
{
    [Serializable]
    public struct HeroConfigInfo
    {
        public HeroType heroType;
        public float speed;
        public int attack;
        public int defense;
        public float attackInterval;
        public float attackRange;
        public int blood;
        public GameObject model;
        public Sprite sprite;
    }

    [Serializable]
    public class HeroConfig
    {
        public HeroConfigInfo[] infos =
        {
            new()
            {
                heroType = HeroType.Warrior,
                speed = 1f,
                attack = 12,
                defense = 8,
                attackInterval = 1f,
                attackRange = 1.5f,
                blood = 120
            },
            new()
            {
                heroType = HeroType.Tank,
                speed = 0.75f,
                attack = 8,
                defense = 15,
                attackInterval = 0.8f,
                attackRange = 1.5f,
                blood = 180
            },
            new()
            {
                heroType = HeroType.Assassin,
                speed = 1.5f,
                attack = 15,
                defense = 5,
                attackInterval = 1.4f,
                attackRange = 1.25f,
                blood = 90
            },
            new()
            {
                heroType = HeroType.Archer,
                speed = 1f,
                attack = 13,
                defense = 4,
                attackInterval = 1.2f,
                attackRange = 5f,
                blood = 85
            },
            new()
            {
                heroType = HeroType.Mage,
                speed = 0.9f,
                attack = 16,
                defense = 3,
                attackInterval = 0.9f,
                attackRange = 4.5f,
                blood = 80
            }
        };

        public HeroConfigInfo GetInfo(HeroType heroType)
        {
            foreach (var heroConfig in infos)
            {
                if (heroType == heroConfig.heroType)
                {
                    return heroConfig;
                }
            }

            return default;
        }
    }
}
