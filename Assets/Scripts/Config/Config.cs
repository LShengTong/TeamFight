using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Config", menuName = "Scriptable Objects/Config")]
    public class Config : ScriptableObject
    {
        public int optionalCardCount = 5;
        public HeroConfig heroConfig;
        public WarehouseConfig warehouseConfig;
        public int battleRow = 4;
        public int battleCol = 7;
        public int benchCount = 8;
        public SlotConfig slotConfig;
        public Color enemyBattleColor = Color.dodgerBlue;
        public Color selectedColor = Color.green;
        public int battleHeroMaxNum = 3;
        public EnemyConfig enemyConfig;
        public Color bloodColor = Color.dodgerBlue;
        public Color enemyBloodColor = Color.red;
    }
}
