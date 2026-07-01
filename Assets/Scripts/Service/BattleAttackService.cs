using Data;
using UnityEngine;

namespace Flow
{
    public class BattleAttackService: MonoBehaviour
    {
        [SerializeField] private Heroes heroesModel;
        [SerializeField] private EnemyHeroes enemyHeroesModel;

        private void Start()
        {
            heroesModel.OnAttack += (hero, target) =>
            {
                if (!enemyHeroesModel.Heroes.TryGetValue(target, out var enemyHero)) return;
                HandleAttack(hero, enemyHero);
            };
            enemyHeroesModel.OnAttack += (hero, target) =>
            {
                var targetHero = heroesModel.GetHero(target);
                if (!targetHero) return;
                HandleAttack(hero, targetHero);
            };
        }

        private static void HandleAttack(Hero attacker, Hero defender)
        {
            var harm = attacker.info.attack * 100 / (100 + defender.info.defense);
            defender.AddBlood(-harm);
        }
    }
}