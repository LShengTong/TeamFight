using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIGold : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldCountText;
        [SerializeField] private Gold gold;

        private void Start()
        {
            Refresh(gold.Value);
            gold.OnGoldChange += Refresh;
        }

        private void Refresh(int goldNum)
        {
            goldCountText.text = "Gold Num: " + goldNum;
        }
    }
}