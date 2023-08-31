using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Weapon[] _weapons;

    private PlayerHUD hud;

    private void Awake()
    {
        GetReferences();
        InitVariables();
    }

    private void GetReferences()
    {
        hud = GetComponent<PlayerHUD>();
    }

    private void Update()
    {
    }

    public void AddItem(Weapon newItem)
    {
        int index = (int)newItem.weaponStyle;
        if (_weapons[index] != null)
            RemoveItem(newItem.weaponStyle);
        _weapons[index] = newItem;

        //Update weaponUI
        hud.UpdateWeaponUI(newItem);
    }

    public void RemoveItem(WeaponStyle weaponStyle)
    {
        int index = (int)weaponStyle;
        Weapon dropedItem = _weapons[index];
        _weapons[index] = null;
        if (dropedItem != null)
        {

        }
    }

    public Weapon GetItem(WeaponStyle weaponStyle)
    {
        int index = (int)weaponStyle;
        return _weapons[index];
    }

    private void InitVariables()
    {
        _weapons = new Weapon[3];
    }

}
