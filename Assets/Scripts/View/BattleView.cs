using JetBrains.Annotations;
using UnityEngine;

public class BattleView : MonoBehaviour
{
    [SerializeField] private Config.Config config;
    [SerializeField] private Vector2 interval;
    [SerializeField] private SlotView slotViewSample;
    [SerializeField] private bool isEnemy;

    private SlotView[][] _slots;

    [ContextMenu("Build")]
    private void Build()
    {
        _slots = new SlotView[config.battleRow][];
        for (var i = 0; i < config.battleRow; i++)
        {
            _slots[i] = new SlotView[config.battleCol];
        }
        var width = interval.x * (config.battleCol - 1);
        var height = interval.y * (config.battleRow - 1);
        for (var i = 0; i < config.battleRow; i++)
        {
            for (var j = 0; j < config.battleCol; j++)
            {
                _slots[i][j] = Instantiate(slotViewSample, transform);
                _slots[i][j].Initialize(isEnemy, new SlotIndex {slotType = SlotType.Battle, x = i, y = j});
                _slots[i][j].transform.localPosition = new Vector3(-width / 2 + interval.x * j,  0.01f, height / 2 - interval.y * i);
            }
        }
    }
        
    private void Awake()
    {
        Build();
    }

    [CanBeNull]
    public SlotView GetSlotView(int row, int col)
    {
        if (row < 0 || row >= _slots.Length) return null;
        if (col < 0 || col >= _slots[0].Length) return null;
        return _slots[row][col];
    }
}
