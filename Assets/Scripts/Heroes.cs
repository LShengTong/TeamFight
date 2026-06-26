
using System;
using System.Collections.Generic;
using UnityEngine;

public struct HeroInfo
{
    public SlotIndex SlotIndex;
    public HeroType HeroType;
}

public class Heroes: MonoBehaviour
{
    [SerializeField] private Config.Config config;

    private int _nextHeroId;
    private readonly Dictionary<int, HeroInfo> _heroes = new();

    public Action<Dictionary<int, HeroInfo>> OnHeroesChange;
    public Dictionary<int, HeroInfo> Infos => _heroes;

    public bool AddHeroToBench(HeroType heroType)
    {
        for (var i = 0; i < config.benchCount; i++)
        {
            if (GetHeroIdBySlotIndex(new SlotIndex{slotType = SlotType.Bench, x = i}) != null) continue;
            _heroes.Add(_nextHeroId++,  new HeroInfo { SlotIndex = new SlotIndex { slotType = SlotType.Bench, x = i }, HeroType = heroType });
            OnHeroesChange?.Invoke(_heroes);
            return true;
        }

        return false;
    }
    
    public void Place(int sourceHeroId, SlotIndex slotIndex)
    {
        var targetHeroId = GetHeroIdBySlotIndex(slotIndex);
        if (targetHeroId != null)
        {
            Exchange(sourceHeroId, targetHeroId.Value);
            return;
        }
        if(!_heroes.TryGetValue(sourceHeroId, out var sourceHeroInfo)) return;
        if(sourceHeroInfo.SlotIndex.slotType != SlotType.Battle && 
           slotIndex.slotType == SlotType.Battle &&
           GetBattleHeroCount() >= config.battleHeroMaxNum) return;
        sourceHeroInfo.SlotIndex = slotIndex;
        _heroes[sourceHeroId] = sourceHeroInfo;
        OnHeroesChange?.Invoke(_heroes);
    }
    
    private void Exchange(int sourceHeroId, int targetHeroId)
    {
        if (!_heroes.ContainsKey(sourceHeroId) || !_heroes.ContainsKey(targetHeroId)) return;
        var sourceHeroInfo = _heroes[sourceHeroId];
        var targetHeroInfo = _heroes[targetHeroId];
        if(sourceHeroInfo.SlotIndex.slotType != SlotType.Battle && 
           targetHeroInfo.SlotIndex.slotType == SlotType.Battle &&
           GetBattleHeroCount() >= config.battleHeroMaxNum) return;
        (sourceHeroInfo.SlotIndex, targetHeroInfo.SlotIndex) = (targetHeroInfo.SlotIndex, sourceHeroInfo.SlotIndex);
        _heroes[sourceHeroId] = sourceHeroInfo;
        _heroes[targetHeroId] = targetHeroInfo;
        OnHeroesChange?.Invoke(_heroes);
    }

    private int? GetHeroIdBySlotIndex(SlotIndex slotIndex)
    {
        foreach (var pair in _heroes)
        {
            if (pair.Value.SlotIndex.Equals(slotIndex))
            {
                return pair.Key;
            }
        }

        return null;
    }

    private int GetBattleHeroCount()
    {
        var count = 0;
        foreach (var pair in _heroes)
        {
            if (pair.Value.SlotIndex.slotType == SlotType.Battle)
            {
                count++;
            }
        }
        return count;
    }
}