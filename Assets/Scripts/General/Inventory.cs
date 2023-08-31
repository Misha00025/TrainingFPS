using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons = new Weapon[3];

    private void Awake()
    {
        GetReferences();
        InitVariables();
    }

    private void GetReferences()
    {

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
        
    }

}
