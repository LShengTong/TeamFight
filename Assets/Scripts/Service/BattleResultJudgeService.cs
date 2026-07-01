using UnityEngine;

namespace Service
{
    public class BattleResultJudgeService : MonoBehaviour
    {
        [SerializeField] private Heroes heroesModel;
        [SerializeField] private EnemyHeroes enemyHeroesModel;

        public void Judge(out bool isEnd, out bool isPlayerWin)
        {
            var playerAllDied = true;
            foreach (var heroId in heroesModel.GetHeroIdBySlotType(SlotType.Battle))
            {
                var hero = heroesModel.GetHero(heroId);
                if(!hero) continue;
                if (hero.Blood <= 0) continue;
                playerAllDied = false;
                break;
            }
            
            var enemyAllDied = true;
            foreach (var pair in enemyHeroesModel.Heroes)
            {
                if (pair.Value.Blood <= 0) continue;
                enemyAllDied = false;
                break;
            }

            isEnd = false;
            isPlayerWin = false;

            if (playerAllDied)
            {
                isEnd = true;
                isPlayerWin = false;
                return;
            }

            if (enemyAllDied)
            {
                isEnd = true;
                isPlayerWin = true;
            }
        }
    }
}