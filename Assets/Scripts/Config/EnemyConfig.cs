using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [System.Serializable]
    public struct EnemyHeroConfigInfo
    {
        public HeroType heroType;
        public int row;
        public int col;
    }

    [System.Serializable]
    public class EnemyConfig
    {
        public List<EnemyHeroConfigInfo> enemyHeroInfos = new()
        {
            new EnemyHeroConfigInfo { heroType = HeroType.Warrior, row = 2, col = 2 },
            new EnemyHeroConfigInfo { heroType = HeroType.Tank, row = 2, col = 3 },
            new EnemyHeroConfigInfo { heroType = HeroType.Archer, row = 3, col = 2 },
            new EnemyHeroConfigInfo { heroType = HeroType.Mage, row = 3, col = 4 }
        };
    }
}
