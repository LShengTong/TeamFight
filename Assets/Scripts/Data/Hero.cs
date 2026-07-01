using System;
using Config;
using UnityEngine;

namespace Data
{
    public class Hero: MonoBehaviour
    {
        [SerializeField] private Config.Config config;
        [SerializeField] private HeroAttackCountDown attackCountDown;
        public HeroType heroType;
        public HeroConfigInfo info;
        public Action<int> OnAttack;
        public Action<int> OnBloodChange;
        
        private int _blood;
        public int Blood => _blood;

        protected void Initialize(HeroType inHeroType)
        {
            heroType = inHeroType;
            info = config.heroConfig.GetInfo(inHeroType);
            _blood = info.blood;
            attackCountDown.Initialize(info.attackInterval);
        }

        public void AddBlood(int amount)
        {
            _blood += amount;
            OnBloodChange?.Invoke(_blood);
        }

        public bool TryAttack(int targetId)
        {
            if (attackCountDown.TryAttack())
            {
                OnAttack?.Invoke(targetId);
                return true;
            }

            return false;
        }
    }
}