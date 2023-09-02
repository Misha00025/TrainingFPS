using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EquipmentManager : IEquipmentManager
{
    [SerializeField] private Transform _weaponHolder = null;
    private Weapon _currentWeapon = null;
    private Weapon _lastWeapon = null;
    private UnityEvent<Weapon> _weaponChanged = new UnityEvent<Weapon>();
    private IInventory _inventory;

    public WeaponStyle CurrentWeaponStyle => _currentWeapon == null ? WeaponStyle.None : _currentWeapon.Style;
    public Weapon CurrentWeapon => _currentWeapon;
    public Transform WeaponHolder => _weaponHolder;

    public void InitVariables(PlayerController playerController)
    {
        _inventory = playerController;
    }    

    public void SetWeaponStyle(WeaponStyle weaponStyle)
    {
        Weapon weapon = _inventory.GetItem(weaponStyle);
        if (weapon != null && weaponStyle != CurrentWeaponStyle)
        {
            _lastWeapon = _currentWeapon;
            _currentWeapon = weapon;
            _weaponChanged.Invoke(weapon);
        }
    }

    public void EquipWeapon()
    {
        Weapon weapon = _inventory.GetItem(CurrentWeaponStyle);
        if (weapon != null)
        {
            weapon.Enable();
            _currentWeapon = weapon;
        }
    }

    public void UnequipWeapon()
    {
        _lastWeapon?.Disable();
    }

    public void SetCurrentWeapon(WeaponStyle weaponStyle) => SetWeaponStyle(weaponStyle);
    public void AddListenerToWeaponChanged(UnityAction<Weapon> action) => _weaponChanged.AddListener(action);
}

public interface IEquipmentManager
{
    Transform WeaponHolder { get; }
    WeaponStyle CurrentWeaponStyle { get; }
    Weapon CurrentWeapon { get; }
    void AddListenerToWeaponChanged(UnityAction<Weapon> action);
    void SetCurrentWeapon(WeaponStyle weaponStyle);
    void EquipWeapon();
    void UnequipWeapon();
}