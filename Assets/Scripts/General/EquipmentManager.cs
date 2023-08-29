using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Transform _weaponHolder = null;

    private Animator _animator;
    private Inventory _inventory;

    private WeaponStyle _weaponStyle;

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
        if (_inventory.GetItem(weaponStyle) != null && weaponStyle != _weaponStyle)
        {
            Weapon weapon = _inventory.GetItem(weaponStyle);
            WeaponType weaponType = weapon.weaponType;
            Debug.Log("Choose " + weaponType.ToString() + "(" + (int)weaponType + ") weapon");
            _animator.SetInteger("WeaponType", (int)weaponType);
            EquipWeapon(weapon.prefab);
            _weaponStyle = weaponStyle;
        }
    }

    private void EquipWeapon(GameObject weaponObject)
    {
        Instantiate(weaponObject, _weaponHolder);
    }

    private void GetReferences()
    {
        _animator = GetComponentInChildren<Animator>();
        _inventory = GetComponentInChildren<Inventory>();
    }
}
