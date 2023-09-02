using System;
using UnityEngine;

[Serializable]
public class WeaponShooting : IShooter
{
    private float _lastShootTime = 0;
    private Camera _camera;
    private IEquipmentManager _equipmentManager;

    public void InitVariables(Camera camera, IEquipmentManager playerController)
    {
        _camera = camera;
        _equipmentManager = playerController;
    }

    private void RaycastShoot(Weapon weapon)
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        float range = 1f;
        if (weapon != null)
            range = weapon.Range;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
        }
        else 
        {
            Debug.Log("Target is out of range");
        }
        weapon.Shoot();
    }

    public void Shoot()
    {
        Weapon weapon = _equipmentManager.CurrentWeapon;
        if (weapon == null) throw new System.Exception("Weapon is Null");
        if (Time.time > _lastShootTime + weapon.FireRate)
        {
            _lastShootTime = Time.time;
            RaycastShoot(weapon);
        }
    }
}
