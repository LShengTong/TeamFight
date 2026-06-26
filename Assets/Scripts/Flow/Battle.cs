using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Flow
{
    public class Battle : MonoBehaviour
    {
        [SerializeField] private Button beginButton;
        [SerializeField] private PlayerSlotHero playerSlotHero;
        [SerializeField] private HeroesView enemyHeroesView;
        [SerializeField] private UIBattleResult uiBattleResult;

        private bool _isBegin;

        private void Start()
        {
            beginButton.onClick.AddListener(BeginBattle);
        }

        private void BeginBattle()
        {
            beginButton.gameObject.SetActive(false);
            _isBegin = true;
            foreach (var heroView in playerSlotHero.GetBattleHeroViews())
            {
                heroView.BeginBattle();
                heroView.OnAttack += target => HandleAttack(heroView, target);
            }
            foreach (var heroView in enemyHeroesView.Heroes.Values)
            {
                heroView.BeginBattle();
                heroView.OnAttack += target => HandleAttack(heroView, target);
            }
        }
        
        private void EndBattle(bool isWin)
        {
            uiBattleResult.gameObject.SetActive(true);
            uiBattleResult.SetResult(isWin);
        }

        private void Update()
        {
            if (!_isBegin) return;

            foreach (var heroView in playerSlotHero.GetBattleHeroViews())
            {
                heroView.FindTarget(enemyHeroesView.Heroes.Values.ToArray());
            }
            foreach (var heroView in enemyHeroesView.Heroes.Values)
            {
                heroView.FindTarget(playerSlotHero.GetBattleHeroViews());
            }

            var playerAllDied = true;
            foreach (var heroView in playerSlotHero.GetBattleHeroViews())
            {
                if (!heroView.IsAlive()) continue;
                playerAllDied = false;
                break;
            }
            
            var enemyAllDied = true;
            foreach (var heroView in enemyHeroesView.Heroes.Values)
            {
                if (!heroView.IsAlive()) continue;
                enemyAllDied = false;
                break;
            }

            if (playerAllDied || enemyAllDied) EndBattle(!playerAllDied);
        }

        private static void HandleAttack(HeroView attacker, HeroView defender)
        {
            var harm = attacker.Info.attack * 100 / (100 + defender.Info.defense);
            defender.ChangeBlood(-harm);
        }
    }
}