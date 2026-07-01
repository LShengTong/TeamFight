using System.Collections.Generic;
using Data;
using JetBrains.Annotations;
using UnityEngine;

public class HeroesView : MonoBehaviour
{
    [SerializeField] private HeroView heroSample;
    [SerializeField] private bool isEnemy;

    private readonly Dictionary<int, HeroView> _heroes = new();
    
    public Dictionary<int, HeroView> Heroes => _heroes;

    public void RefreshHeroes(Dictionary<int, Hero> heroes)
    {
        foreach (var heroId in heroes.Keys)
        {
            if (_heroes.ContainsKey(heroId)) continue;
            var newHero = Instantiate(heroSample, transform);
            newHero.name = (isEnemy ? "Enemy" : "Player") + "_Hero_" + heroId;
            _heroes.Add(heroId, newHero);
            newHero.Initialize(heroId, isEnemy, heroes[heroId]);
        }

        foreach (var heroId in _heroes.Keys)
        {
            if (heroes.ContainsKey(heroId)) continue;
            Destroy(_heroes[heroId]);
            _heroes.Remove(heroId);
        }
    }

    [CanBeNull]
    public HeroView GetHero(int heroId)
    {
        return _heroes.GetValueOrDefault(heroId);
    }
}