using System;
using System.Collections.Generic;
using Data;
using JetBrains.Annotations;
using UnityEngine;

public class Heroes: MonoBehaviour
{
    [SerializeField] private Config.Config config;
    [SerializeField] private PlayerHero playerHeroSample;

    private int _nextHeroId;
    private readonly Dictionary<int, PlayerHero> _heroes = new();

    public Action<Dictionary<int, PlayerHero>> OnHeroesChange;
    public Dictionary<int, PlayerHero> Infos => _heroes;
    
    public Action<PlayerHero, int> OnAttack;

    public void AddHero(SlotIndex slotIndex, HeroType heroType)
    {
        var hero = Instantiate(playerHeroSample, transform);
        hero.Initialize(heroType, slotIndex);
        hero.OnAttack += id => OnAttack(hero, id);
        _heroes.Add(_nextHeroId++, hero);
        OnHeroesChange?.Invoke(_heroes);
    }

    public int? GetHeroIdBySlotIndex(SlotIndex slotIndex)
    {
        foreach (var pair in _heroes)
        {
            if (pair.Value.slotIndex.Equals(slotIndex))
            {
                return pair.Key;
            }
        }

        return null;
    }
    
    public int[] GetHeroIdBySlotType(SlotType slotType)
    {
        var heroIds = new List<int>();
        foreach (var pair in _heroes)
        {
            if (pair.Value.slotIndex.slotType == slotType)
            {
                heroIds.Add(pair.Key);
            }
        }

        return heroIds.ToArray();
    }
    
    public void SetSlotIndex(int heroId, SlotIndex slotIndex)
    {
        if (!_heroes.TryGetValue(heroId, out var hero)) return;
        hero.slotIndex = slotIndex;
        OnHeroesChange?.Invoke(_heroes);
    }
    
    public void SetSlotIndex(int[] heroId, SlotIndex[] slotIndex)
    {
        if(heroId.Length != slotIndex.Length) return;
        for (var i = 0; i < heroId.Length; i++)
        {
            SetSlotIndex(heroId[i], slotIndex[i]);
        }
        OnHeroesChange?.Invoke(_heroes);
    }

    [CanBeNull]
    public PlayerHero GetHero(int heroId)
    {
        return _heroes.GetValueOrDefault(heroId);
    }
}
