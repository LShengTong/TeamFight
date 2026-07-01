using Data;
using UnityEngine;

namespace Service
{
    public static class BattleHeroActionService
    {
        public static void Action(Hero model, HeroView view, HeroView[] candidates)
        {
            if (model.Blood <= 0) return;
            var target = FindTarget(view, candidates, out var minDistance);
            if (target == null)
            {
                view.SetWalking(false);
                return;
            }

            if (minDistance < view.Info.attackRange)
            {
                view.SetWalking(false);
                if (model.TryAttack(target.id))
                {
                    view.Attack();
                }
                return;
            }
            
            view.SetWalking(true);
            view.SetNavAgentDestination(target.transform.position);
        }
        
        private static HeroView FindTarget(HeroView view, HeroView[] candidates, out float minDistance)
        {
            minDistance = float.MaxValue;
            HeroView minDistanceHero = null;
            foreach (var hero in candidates)
            {
                if (!hero.gameObject.activeSelf) continue;
                var distance = Vector3.Distance(view.transform.position, hero.transform.position);
                if (distance >= minDistance) continue;
                minDistance = distance;
                minDistanceHero = hero;
            }
            return minDistanceHero;
        }
    }
}