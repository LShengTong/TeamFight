using System;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyHeroInfo
{
   public HeroType HeroType;
   public int Row;
   public int Col;
}

public class EnemyHeroes : MonoBehaviour
{
   [SerializeField] private Config.Config config;
   
   private readonly Dictionary<int, EnemyHeroInfo> _enemyHeroInfos = new();

   public Action<Dictionary<int, EnemyHeroInfo>> OnEnemyHeroInfoChange;
   public Dictionary<int, EnemyHeroInfo> Infos => _enemyHeroInfos;

   private void Awake()
   {
      var index = 0;
      foreach (var info in config.enemyConfig.enemyHeroInfos)
      {
         _enemyHeroInfos.Add(index++, new EnemyHeroInfo{HeroType = info.heroType, Row = info.row, Col = info.col});
      }
      OnEnemyHeroInfoChange?.Invoke(_enemyHeroInfos);
   }
}
