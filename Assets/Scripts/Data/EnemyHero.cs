using UnityEngine;

namespace Data
{
    public class EnemyHero : Hero
    {
        public int col;
        public int row;
        
        public void Initialize(HeroType inHeroType, int inRow, int inCol) 
        {
            Initialize(inHeroType);
            row = inRow;
            col = inCol;
        }        
    }
}