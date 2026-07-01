using UnityEngine;

namespace Data
{
    public class HeroAttackCountDown: MonoBehaviour
    {
        private float _countDown;
        private float _attackInterval;

        public void Initialize(float attackInterval)
        {
            _attackInterval = attackInterval;
        }

        private void Update()
        {
            if (_countDown <= 0) return;
            _countDown -= Time.deltaTime;
        }

        public bool TryAttack()
        { 
            if (_countDown > 0) return false;
            _countDown = _attackInterval;
            return true;
        }
    }
}