using System;
using Config;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HeroView: MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Config.Config config;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFillArea;
    
    private HeroConfigInfo _info;
    private float _attackCountDown;
    private int _blood;

    public int id;
    public bool isEnemy;
    public Action<HeroView> OnAttack;
    public HeroConfigInfo Info => _info;

    public void Initialize(int inId, bool inIsEnemy, HeroType heroType)
    {
        id = inId;
        isEnemy = inIsEnemy;
        _info = config.heroConfig.GetInfo(heroType);
        _blood = _info.blood;
        var model = Instantiate(_info.model, transform);
        if(isEnemy) model.transform.Rotate(0, 180, 0);
        navMeshAgent.speed = _info.speed;
        navMeshAgent.stoppingDistance = _info.attackRange;
        navMeshAgent.enabled = false;
        sliderFillArea.color = isEnemy ? config.enemyBloodColor : config.bloodColor;
    }

    public void ChangeBlood(int delta)
    {
        _blood += delta;
        slider.value = _blood * 1f / _info.blood;
        if (_blood <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public bool IsAlive()
    {
        return _blood > 0;
    }

    public void BeginBattle()
    {
        navMeshAgent.enabled = true;
    }

    private void Update()
    {
        if (_blood <= 0) return;
        if (_attackCountDown > 0)
        {
             _attackCountDown -= Time.deltaTime;
        }
    }

    public void FindTarget(HeroView[] candidates)
    {
        if (_blood <= 0) return;
        var minDistance = float.MaxValue;
        HeroView minDistanceHero = null;
        foreach (var hero in candidates)
        {
            if (!hero.IsAlive()) continue;
            var distance = Vector3.Distance(transform.position, hero.transform.position);
            if (!(distance < minDistance)) continue;
            minDistance = distance;
            minDistanceHero = hero;
        }

        if (minDistanceHero && minDistance < _info.attackRange && _attackCountDown <= 0)
        {
            _attackCountDown = _info.attackInterval;
            OnAttack?.Invoke(minDistanceHero);
        }
        if (minDistanceHero)
        {
            navMeshAgent.SetDestination(minDistanceHero.transform.position);
        }
    }
}