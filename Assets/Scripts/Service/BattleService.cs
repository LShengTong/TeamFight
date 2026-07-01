using System.Collections.Generic;
using System.Linq;
using Data;
using Service;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Flow
{
    public class BattleService : MonoBehaviour
    {
        [SerializeField] private Button beginButton;
        [SerializeField] private HeroesView heroesView;
        [SerializeField] private Heroes heroesModel;
        [SerializeField] private HeroesView enemyHeroesView;
        [SerializeField] private EnemyHeroes enemyHeroesModel;
        [SerializeField] private UIBattleResult uiBattleResult;
        [SerializeField] private BattleResultJudgeService battleResultJudgeService;

        private bool _isBegin;

        private void Start()
        {
            beginButton.onClick.AddListener(BeginBattle);
        }

        private void BeginBattle()
        {
            beginButton.gameObject.SetActive(false);
            _isBegin = true;
            foreach (var heroId in heroesModel.GetHeroIdBySlotType(SlotType.Battle))
            {
                var view = heroesView.GetHero(heroId);
                if (!view) continue;
                view.SetNavMeshAgentEnable(true);
            }
            foreach (var (key, _) in enemyHeroesModel.Heroes)
            {
                var view = enemyHeroesView.GetHero(key);
                if (!view) continue;
                view.SetNavMeshAgentEnable(true);
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
            
            var heroViews = new List<HeroView>();
            foreach (var heroId in heroesModel.GetHeroIdBySlotType(SlotType.Battle))
            {
                var model = heroesModel.GetHero(heroId);
                var view = heroesView.GetHero(heroId);
                if(!model || !view) continue;
                BattleHeroActionService.Action(model, view, enemyHeroesView.Heroes.Values.ToArray());
                heroViews.Add(view);
            }
            foreach (var pair in enemyHeroesModel.Heroes)
            {
                var model = pair.Value;
                var view = enemyHeroesView.GetHero(pair.Key);
                if (!model || !view) continue;
                BattleHeroActionService.Action(model, view, heroViews.ToArray());
            }
            
            battleResultJudgeService.Judge(out var isEnd, out var isWin);
            if (isEnd) EndBattle(isWin);
        }
    }
}