using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    public class BuyExpService : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private Gold gold;
        [SerializeField] private Level level;
        [SerializeField] private Button buyExpButton;
        
        private void Start()
        {
            buyExpButton.onClick.AddListener(HandleBuyExp);
        }

        private void HandleBuyExp()
        {
            if (!gold.ConsumeGold(config.buyExpCost)) return;
            level.AddExp(config.buyExpValue);
        }
    }
}