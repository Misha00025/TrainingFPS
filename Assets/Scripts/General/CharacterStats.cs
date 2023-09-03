using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class CharacterStats : IHealthState, IDamagable
{
    [SerializeField] private int _maxHealth;
    private int _health;

    private UnityEvent<IHealthState> _healthChanged = new UnityEvent<IHealthState>();
    private UnityEvent<IHealthState> _die = new UnityEvent<IHealthState>();

    public virtual void InitVariables()
    {
        SetCurrentHealth(_maxHealth);
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public int Health => _health;
    public int MaxHealth => _maxHealth;

    protected void SetCurrentHealth(int health)
    {
        _health = health;
        CheckHealth();
        _healthChanged.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        int healthAfterDamage = _health - damage;
        SetCurrentHealth(healthAfterDamage);
    }

    public void TakeHeal(int heal)
    {
        int healthAfterHeal = _health + heal;
        SetCurrentHealth(healthAfterHeal);
    }

    public void AddListenerToHealthChange(UnityAction<IHealthState> action)
    {
        _healthChanged.AddListener(action);
    }

    protected virtual void Die()
    {
        _die.Invoke(this);
    }

    public void AddListenerToDie(UnityAction<IHealthState> action)
    {
        _die.AddListener(action);
    }
}

public interface IHealthState
{
    int Health { get; }
    int MaxHealth { get; }

    void AddListenerToHealthChange(UnityAction<IHealthState> action);
    void AddListenerToDie(UnityAction<IHealthState> action);
}

public interface IDamagable
{
    void TakeDamage(int damage);
}

