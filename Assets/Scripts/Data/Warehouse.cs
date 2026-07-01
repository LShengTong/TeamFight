using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private Config.Config config;
    
    public readonly Dictionary<HeroType, int> Cards = new();

    private void Awake()
    {
        foreach (var info in config.warehouseConfig.infos)
        {
            Cards.Add(info.heroType, info.cardCount);
        }
    }

    public void Add(HeroType[] cards)
    {
        foreach (var hero in cards)
        {
            if (!Cards.ContainsKey(hero)) return;
            Cards[hero] += 1;
        }
    }
}
