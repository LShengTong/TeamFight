using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BenchView : MonoBehaviour
{
    [SerializeField] private Config.Config config;
    [SerializeField] private float interval;
    [SerializeField] private SlotView slotViewSample;

    private SlotView[] _slots;

    [ContextMenu("Build")]
    private void Build()
    {
        _slots = new SlotView[config.benchCount];
        var width = interval * (config.benchCount - 1);
        for (var i = 0; i < config.benchCount; i++)
        {
            _slots[i] = Instantiate(slotViewSample, transform);
            _slots[i].Initialize(false, new SlotIndex { slotType = SlotType.Bench, x = i });
            _slots[i].transform.localPosition = new Vector3(-width / 2 + interval * i,  0.01f, 0);
        }
    }
        
    private void Start()
    {
        Build();
    }
    
    [CanBeNull]
    public SlotView GetSlotView(int index)
    {
        if (index < 0 || index >= _slots.Length) return null;
        return _slots[index];
    }
}
