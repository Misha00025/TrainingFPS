using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    private IEquipmentManager _equipmentManager;
    private Animator _animator;

    private void Start()
    {
        _equipmentManager = GetComponentInParent<PlayerController>();
        _animator = GetComponentInParent<Animator>();

        _equipmentManager.AddListenerToWeaponChanged(WeaponChanged);
    }

    private void WeaponChanged(Weapon weapon)
    {
        _animator.SetInteger("WeaponType", (int)weapon.Type);
        _animator.SetTrigger("UnequipWeapon");
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
