using UnityEngine;

namespace Config
{
    [System.Serializable]
    public struct SlotInfo
    {
        public SlotType type;
        public Color color;
    }

    [System.Serializable]
    public class SlotConfig
    {
        public SlotInfo[] infos =
        {
            new(){type = SlotType.Bench, color = Color.yellow},
            new(){type = SlotType.Battle, color = Color.red}
        };

        public Color GetColor(SlotType status)
        {
            foreach(var info in infos)
            {
                if (info.type == status)
                {
                    return info.color;
                }
            }
            return Color.white;
        }
    }
}