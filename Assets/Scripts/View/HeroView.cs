using System;
using Config;
using Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HeroView: MonoBehaviour
{
    private static readonly int Walk = Animator.StringToHash("walking");
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Config.Config config;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFillArea;

    private HeroConfigInfo _info;
    private Animator _animator;
    private bool _walking;

    public int id;
    public bool isEnemy;
    public HeroConfigInfo Info => _info;

    public void Initialize(int inId, bool inIsEnemy, Hero hero)
    {
        id = inId;
        isEnemy = inIsEnemy;
        _info = config.heroConfig.GetInfo(hero.heroType);
        var model = Instantiate(_info.model, transform);
        _animator = model.GetComponent<Animator>();
        navMeshAgent.speed = _info.speed;
        navMeshAgent.stoppingDistance = _info.attackRange;
        navMeshAgent.enabled = false;
        sliderFillArea.color = isEnemy ? config.enemyBloodColor : config.bloodColor;
        hero.OnBloodChange += HandleBloodChange;
    }

    private void HandleBloodChange(int blood)
    {
        slider.value = blood * 1f / _info.blood;
        if (blood <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetNavMeshAgentEnable(bool enable)
    {
        navMeshAgent.enabled = enable;
    }

    public void SetNavAgentDestination(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }
    
    public void SetWalking(bool walking)
    {
        if (walking == _walking) return;
        _animator.SetBool(Walk, walking);
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            _animator.Play(walking ? "walk" : "idle");
        }
        _walking = walking;
        navMeshAgent.isStopped = !walking;
    }
    
    public void Attack()
    {
        _animator.Play("attack");
    }
}