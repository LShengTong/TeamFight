using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBattleResult : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetResult(bool isWin)
        {
            text.text = isWin ? "Win" : "Lose";
        }
    }
}
