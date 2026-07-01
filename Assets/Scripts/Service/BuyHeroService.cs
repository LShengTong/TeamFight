using System.Linq;
using Data;
using UI;
using UnityEngine;

namespace Service
{
    public class BuyHeroService : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private UICards uiCards;
        [SerializeField] private OptionalCards optionalCards;
        [SerializeField] private Heroes heroes;
        [SerializeField] private Gold gold;

        private void Start()
        {
            for (var i = 0; i < uiCards.Cards.Length; i++)
            {
                var lambdaIndex = i;
                uiCards.Cards[i].Button.onClick.AddListener(() =>
                {
                    HandleClickCard(lambdaIndex);
                });
            }
        }

        private void HandleClickCard(int index)
        {
            var heroType = optionalCards.Get(index);
            var price = config.heroConfig.GetInfo(heroType).price;
            if (!gold.ConsumeGold(price)) return;
            for (var i = 0; i < config.benchCount; i++)
            {
                SlotIndex slotIndex = new() { slotType = SlotType.Bench, x = i };
                if (heroes.GetHeroIdBySlotIndex(slotIndex) != null) continue;
                heroes.AddHero(slotIndex, optionalCards.Get(index));
                break;
            }
            optionalCards.Set(index, HeroType.Invalid);
        }
    }
}