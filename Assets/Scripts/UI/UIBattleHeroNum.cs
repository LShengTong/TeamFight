using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIBattleHeroNum : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI text;
        [SerializeField] public Heroes heroes;
        [SerializeField] public Level level;

        private void Start()
        {
            Refresh();
            level.OnLevelInfoChange += HandleLevelChange;
            heroes.OnHeroesChange += HandleHeroesChange;
        }

        private void HandleLevelChange(int _1, int _2)
        {
            Refresh();
        }
        
        private void HandleHeroesChange(Dictionary<int,PlayerHero> _)
        {
            Refresh();
        }

        private void Refresh()
        {
            text.text = "Battle Hero Num: " + heroes.GetHeroIdBySlotType(SlotType.Battle).Length + " / "
                        + level.LevelValue;
        }
    }
}