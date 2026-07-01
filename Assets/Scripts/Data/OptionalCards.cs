using System;
using UnityEngine;

public class OptionalCards: MonoBehaviour
{
    [SerializeField] private Config.Config config;
    
    private HeroType[] _value = Array.Empty<HeroType>();

    public Action<HeroType[]> OnChange;

    public void Set(HeroType[] value)
    {
        _value = value;
        OnChange?.Invoke(_value);
    }
    
    public void Set(int index, HeroType value)
    {
        if (index < 0 || index >= _value.Length) return;
        _value[index] = value;
        OnChange?.Invoke(_value);
    }

    public HeroType[] Get()
    {
        return _value;
    }
    
    public HeroType Get(int index)
    {
        if (index >= 0 && index < _value.Length)
        {
            return _value[index];
        }
        return HeroType.Invalid;
    }
}
