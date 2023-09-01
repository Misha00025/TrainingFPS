using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IMover, IHealthState, IDamagable, IInventory, IEquipmentManager
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private EquipmentManager _equipment;

    [Header("References")]
    private Animator anim;
    public Inventory Inventory => _inventory;
    public Vector3 Direction => _mover.Direction;
    public int Health => _stats.Health;
    public int MaxHealth => _stats.MaxHealth;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        anim = GetComponentInChildren<Animator>();

        _mover.InitVariables(GetComponent<CharacterController>());
        _stats.InitVariables();
        _inventory.InitVariables();
    }

    public void SetRuning(bool run)
    {
        _mover.SetRuning(run);
        if (_mover.Direction == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f, 0.2f, Time.deltaTime);
        }
        else if (!run)
        {
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
        }
    }

    public void Move(Vector3 direction, bool jump) => _mover.Move(direction, jump);
    public void AddListenerToHealthChange(UnityAction<IHealthState> action) => _stats.AddListenerToHealthChange(action);
    public void TakeDamage(int damage) => _stats.TakeDamage(damage);
    public void AddItem(Weapon weapon) => _inventory.AddItem(weapon);
    public void RemoveItem(WeaponStyle weaponStyle) => _inventory.RemoveItem(weaponStyle);
    public Weapon GetItem(WeaponStyle weaponStyle) => _inventory.GetItem(weaponStyle);

    public void SetCurrentWeapon(WeaponStyle weaponStyle) =>  throw new NotImplementedException();
    public void EquipWeapon() => throw new NotImplementedException();
    public void UnequipWeapon() => throw new NotImplementedException();
}
