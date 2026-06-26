using UnityEngine;

public class SlotView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Config.Config config;
    
    private SlotType _slotType;
    private bool _selected;
    
    public bool isEnemy;
    public SlotIndex index;

    public void Initialize(bool inIsEnemy, SlotIndex inIndex)
    {
        index = inIndex;
        isEnemy = inIsEnemy;
        _slotType = index.slotType;
        Refresh();
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;
        Refresh();
    }

    private void Refresh()
    {
        if (isEnemy)
        {
            sprite.color = config.enemyBattleColor;
            return;
        }
        sprite.color = _selected ? config.selectedColor : config.slotConfig.GetColor(_slotType);
    }
}
