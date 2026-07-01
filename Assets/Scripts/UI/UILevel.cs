using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UILevel : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Level level;

        private void Start()
        {
            Refresh(level.LevelValue, level.CurrExp);
            level.OnLevelInfoChange += Refresh;
        }

        private void Refresh(int levelValue, int exp)
        {
            if (levelValue < config.levelExp.Count)
            {
                levelText.text = "Level: " + levelValue + " Exp: " + exp + "/" + 
                                 config.levelExp[levelValue];
            }
            else
            {
                levelText.text = "Level: " + levelValue + " Exp: max";
            }
        }
    }
}