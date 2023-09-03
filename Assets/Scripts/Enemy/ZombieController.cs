using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour, IHealthState, IDamagable, IDamager, IAttacker
{
    [SerializeField] private ZombieStats _stats;
    [SerializeField] private ZombieAttacker _attacker;

    public int Health => _stats.Health;
    public int MaxHealth => _stats.MaxHealth;
    public int Damage => _stats.Damage;
    public float AttackSpeed => _stats.AttackSpeed;
    public float AttackDistance => _stats.AttackDistance;

    private void Start()
    {
        _stats.InitVariables();
        _attacker.InitVariables(this);

        _stats.AddListenerToDie((IHealthState health) => { Destroy(gameObject); });
    }

    public void AddListenerToHealthChange(UnityAction<IHealthState> action) => _stats.AddListenerToHealthChange(action);
    public void Attack(Transform target) => _attacker.Attack(target);
    public void TakeDamage(int damage) => _stats.TakeDamage(damage);
    public void DropAttack() => _attacker.DropAttack();
    public void AddListenerToDie(UnityAction<IHealthState> action) => _stats.AddListenerToDie(action);
}
