using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private BarWithCounter _healthBar;
    [SerializeField] private WeaponUI weaponUI;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerController.AddListenerToWeaponChanged(UpdateWeaponUI);
        _playerController.AddListenerToHealthChange(UpdateHealthBar);
    }
    private void UpdateHealthBar(IHealthState health)
    {
        _healthBar.UpdateBar(health.Health, health.MaxHealth);
    }

    public void UpdateWeaponUI(Weapon newWeapon)
    {
        weaponUI.UpdateInfo(newWeapon.Config.icon, newWeapon.Count, newWeapon.MaxCount);
    }
}
