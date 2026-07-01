using System;
using UnityEngine;

namespace Data
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Config.Config config;

        private int _level = 1;
        private int _currExp;

        public Action<int, int> OnLevelInfoChange;
        public int LevelValue => _level;
        public int CurrExp => _currExp;

        public bool AddExp(int exp)
        {
            _currExp += exp;
            if (_level >= config.levelExp.Count)
            {
                OnLevelInfoChange?.Invoke(_level, _currExp);
                return false;
            }
            
            while (_currExp >= config.levelExp[_level])
            {
                _currExp -= config.levelExp[_level];
                _level += 1;
            }
            
            OnLevelInfoChange?.Invoke(_level, _currExp);

            return true;
        }
    }
}