using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    private EquipmentManager _equipmentManager;

    private void Start()
    {
        _equipmentManager = GetComponentInParent<EquipmentManager>();
    }

    public void DestroyWeapon()
    {
        _equipmentManager.UnequipWeapon();
    }

    public void InstantiateWeapon()
    {
        _equipmentManager.EquipWeapon();
    }
}
