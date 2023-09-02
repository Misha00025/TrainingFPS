using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Inventory : IInventory
{
    [SerializeField] private WeaponScriptableObject[] _defaultWeapons;
    private Weapon[] _weapons = new Weapon[3];

    private PlayerController _playerController;

    public void InitVariables(PlayerController playerController)
    {
        _playerController = playerController;
        foreach (var weapon in _defaultWeapons)
        {
            if (weapon != null) AddItem(weapon);
        }
    }

    public void AddItem(WeaponScriptableObject newItem)
    {
        int index = (int)newItem.weaponStyle;
        if (_weapons[index] != null)
            RemoveItem(newItem.weaponStyle);
        _weapons[index] = new Weapon(newItem, _playerController.WeaponHolder);
    }

    public void RemoveItem(WeaponStyle weaponStyle)
    {
        int index = (int)weaponStyle;
        _weapons[index] = null;
    }

    public Weapon GetItem(WeaponStyle weaponStyle)
    {
        int index = (int)weaponStyle;
        return _weapons[index];
    }

}

public interface IInventory
{
    void AddItem(WeaponScriptableObject weapon);
    void RemoveItem(WeaponStyle weaponStyle);
    Weapon GetItem(WeaponStyle weaponStyle);
}