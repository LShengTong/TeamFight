using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    public class RefreshOptionalCardsService : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private OptionalCards optionalCards;
        [SerializeField] private Warehouse warehouse;
        [SerializeField] private Button cardRefreshButton;
        [SerializeField] private Gold gold;

        private void Start()
        {
            Refresh();
            cardRefreshButton.onClick.AddListener(BuyRefresh);
        }

        private void BuyRefresh()
        {
            if (!gold.ConsumeGold(config.refreshCost)) return;
            warehouse.Add(optionalCards.Get());
            Refresh();
        }
        
        private void Refresh()
        {
            var allCards = new List<HeroType>();
            foreach (var pair in warehouse.Cards)
            {
                for (var i = 0; i < pair.Value; i++)
                {
                    allCards.Add(pair.Key);
                }
            }
            var selectedCards = new HeroType[config.optionalCardCount];
            for (var i = 0; i < config.optionalCardCount; i++)
            {
                var index = Random.Range(0, allCards.Count);
                selectedCards[i] = allCards[index];
                warehouse.Cards[allCards[index]] -= 1;
                allCards.RemoveAt(index);
            }
            optionalCards.Set(selectedCards);
        }
    }
}