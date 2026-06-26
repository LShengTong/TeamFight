using System;
using UnityEngine;

public class OptionalCards: MonoBehaviour
{
    [SerializeField] private Config.Config config;
    
    private HeroType[] _value;

    public Action<HeroType[]> OnChange;

    private void Start()
    {
        _value ??= new HeroType[config.optionalCardCount];
    }

    public void Set(HeroType[] value)
    {
        _value = value;
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

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _value.Length) return;
        _value[index] = HeroType.Invalid;
        OnChange?.Invoke(_value);
    }
}
