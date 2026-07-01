using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class EnemyHeroes : MonoBehaviour
{
   [SerializeField] private Config.Config config;
   [SerializeField] private EnemyHero enemyHeroSample;
   
   private readonly Dictionary<int, EnemyHero> _enemyHeroInfos = new();

   public Action<Dictionary<int, EnemyHero>> OnEnemyHeroInfoChange;
   public Dictionary<int, EnemyHero> Heroes => _enemyHeroInfos;
   public Action<EnemyHero, int> OnAttack;

   private void Awake()
   {
      var index = 0;
      foreach (var info in config.enemyConfig.enemyHeroInfos)
      {
         var hero = Instantiate(enemyHeroSample, transform);
         hero.Initialize(info.heroType, info.row, info.col);
         _enemyHeroInfos.Add(index, hero);
         hero.OnAttack += target => OnAttack(hero, target);
         index++;
      }
      OnEnemyHeroInfoChange?.Invoke(_enemyHeroInfos);
   }
}
