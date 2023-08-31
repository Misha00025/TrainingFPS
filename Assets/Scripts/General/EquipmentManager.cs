using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder = null;
    [SerializeField] private Weapon _defaultWeapon;

    private Animator _animator;
    private Inventory _inventory;

    private WeaponStyle _weaponStyle;
    private GameObject _currentWeapon = null;

    public Transform currentWeaponBarel = null;

    public WeaponStyle WeaponStyle => _weaponStyle;


    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        WeaponStyle weaponStyle = _weaponStyle;
        bool pressed = false;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponStyle = WeaponStyle.Primary;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponStyle = WeaponStyle.Secondary;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponStyle = WeaponStyle.Melee;
        }

        SetWeaponStyle(weaponStyle);
    }

    public void SetWeaponStyle(WeaponStyle weaponStyle)
    {
        Weapon weapon = _inventory.GetItem(weaponStyle);
        if (weapon != null && weaponStyle != _weaponStyle)
        {
            _weaponStyle = weaponStyle;
            _animator.SetTrigger("UnequipWeapon");
            _animator.SetInteger("WeaponType", (int)weapon.weaponType);
        }
    }

    public void EquipWeapon()
    {
        Weapon weapon = _inventory.GetItem(_weaponStyle);
        if (weapon != null)
        {
            _currentWeapon = Instantiate(weapon.prefab, _weaponHolder);
            currentWeaponBarel = _currentWeapon.transform.GetChild(0);
        }
    }

    public void UnequipWeapon()
    {
        Weapon weapon = _inventory.GetItem(_weaponStyle);
        if (weapon != null)
        {
            Destroy(_currentWeapon);
        }
    }

    private void GetReferences()
    {
        _animator = GetComponentInChildren<Animator>();
        _inventory = GetComponentInChildren<Inventory>();
        _inventory.AddItem(_defaultWeapon);
        _weaponStyle = _defaultWeapon.weaponStyle;
        SetWeaponStyle(_weaponStyle);
    }
}
