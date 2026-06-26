using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private Config.Config config;
    
    private readonly Dictionary<HeroType, int> _cards = new();

    private void Awake()
    {
        foreach (var info in config.warehouseConfig.infos)
        {
            _cards.Add(info.heroType, info.cardCount);
        }
    }

    public HeroType[] RandomTakeout(int count)
    {
        var allCards = new List<HeroType>();
        foreach (var pair in _cards)
        {
            for (var i = 0; i < pair.Value; i++)
            {
                allCards.Add(pair.Key);
            }
        }
        var selectedCards = new HeroType[count];
        for (var i = 0; i < count; i++)
        {
            var index = Random.Range(0, allCards.Count);
            selectedCards[i] = allCards[index];
            _cards[allCards[index]] -= 1;
            allCards.RemoveAt(index);
        }
        return selectedCards;
    }
}
