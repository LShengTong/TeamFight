using System;
using UnityEngine;

namespace Data
{
    public class Gold : MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        private int _gold;

        public Action<int> OnGoldChange;
        public int Value => _gold;

        private void Start()
        {
            _gold = config.initialGoldNum;
            OnGoldChange?.Invoke(_gold);
        }
        
        public void AddGold(int gold)
        {
            _gold += gold;
            OnGoldChange?.Invoke(_gold);
        }
        
        public bool ConsumeGold(int gold)
        {
            if (gold > _gold) return false;
            _gold -= gold;
            OnGoldChange?.Invoke(_gold);
            return true;
        }
    }
}