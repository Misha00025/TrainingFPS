using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour, IEquipmentManager
{
    [SerializeField] private Transform _weaponHolder = null;

    private Animator _animator;
    private IInventory _inventory;
    private PlayerHUD _hud;

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
        if (weapon != null && (weaponStyle != _weaponStyle || (int)weapon.weaponType != _animator.GetInteger("WeaponType")))
        {
            _weaponStyle = weaponStyle;
            _animator.SetInteger("WeaponType", (int)weapon.weaponType);
            _animator.SetTrigger("UnequipWeapon");
        }
    }

    public void EquipWeapon()
    {
        Weapon weapon = _inventory.GetItem(_weaponStyle);
        if (weapon != null)
        {
            _currentWeapon = Instantiate(weapon.prefab, _weaponHolder);
            currentWeaponBarel = _currentWeapon.transform.GetChild(0);

            //Update weaponUI
            _hud.UpdateWeaponUI(weapon);
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
        _inventory = GetComponentInChildren<PlayerController>();
        _hud = GetComponent<PlayerHUD>();
    }

    public void SetCurrentWeapon(WeaponStyle weaponStyle)
    {
        SetWeaponStyle(weaponStyle);
    }
}

public interface IEquipmentManager
{
    void SetCurrentWeapon(WeaponStyle weaponStyle);
    void EquipWeapon();
    void UnequipWeapon();
}