using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    private float _lastShootTime = 0;
    private Camera _camera;
    private Inventory _inventory;
    private EquipmentManager _equipmentManager;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void GetReferences()
    {
        _camera = GetComponentInChildren<Camera>();
        _inventory = GetComponent<PlayerController>().Inventory;
        _equipmentManager = GetComponent<EquipmentManager>();
    }

    private void RaycastShoot(Weapon weapon)
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        float range = 1f;
        if (weapon != null)
            range = weapon.range;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);
        }
        else 
        {
            Debug.Log("Target is out of range");
        }

        if (weapon.muzzleFlashParticles != null)
            Instantiate(weapon.muzzleFlashParticles, _equipmentManager.currentWeaponBarel);
    }

    private void Shoot()
    {
        Weapon weapon = _inventory.GetItem(_equipmentManager.WeaponStyle);
        if (weapon == null) return;
        if (Time.time > _lastShootTime + weapon.fireRate)
        {
            _lastShootTime = Time.time;
            RaycastShoot(weapon);
        }
    }
}
