using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour, IHealthState, IDamagable
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    private UnityEvent<IHealthState> _healthChanged = new UnityEvent<IHealthState>();
    
    private void Start()
    {
        InitVariables();
    }

    private void CheckHealth()
    {
        if (_health < 0)
            _health = 0;
        if (_health > _maxHealth)
            _health = _maxHealth;
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

    public void InitVariables()
    {
        SetCurrentHealth(_maxHealth);
    }

    public void AddListenerToHealthChange(UnityAction<IHealthState> action)
    {
        _healthChanged.AddListener(action);
    }
}
