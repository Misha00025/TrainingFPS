using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieStats : CharacterStats, IDamager
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackDistance;

    private bool _canAttack;

    public int Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float AttackDistance => _attackDistance;
}

[Serializable]
public class ZombieAttacker : IDamager, IAttacker, IDamageDealler
{
    private IDamager _damager;
    private float _tryAttackTime = 0f;
    public int Damage => _damager.Damage;
    public float AttackSpeed => _damager.AttackSpeed;
    public float AttackDistance => _damager.AttackDistance;

    public void InitVariables(IDamager damager) 
    {
        _damager = damager; 
    }

    public void Attack(Transform target)
    {
        if (target == null) return;

        _tryAttackTime += Time.deltaTime;

        if (_tryAttackTime < (1 / AttackSpeed )) return;

        IDamagable damagableTarget = target.GetComponent<IDamagable>();
        if (damagableTarget == null) return;
        DealDamage(damagableTarget);
        DropAttack();
    }

    public void DealDamage(IDamagable target)
    {
        target.TakeDamage(Damage);
    }

    public void DropAttack()
    {
        _tryAttackTime = 0;
    }
}

public interface IDamager
{
    int Damage { get; }
    float AttackSpeed { get; }
    float AttackDistance { get; }
}

public interface IDamageDealler
{
    void DealDamage(IDamagable target);
}

interface IAttacker
{    
    void Attack(Transform target);
    void DropAttack();
}
