using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleResult : MonoBehaviour
{
    public Button button;
    
    [SerializeField] private TextMeshProUGUI text;

    public void SetResult(bool isWin)
    {
        text.text = isWin ? "Win" : "Lose";
    }
}
