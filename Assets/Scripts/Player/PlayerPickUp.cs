using System;
using UnityEngine;


[Serializable]
public class PlayerPickUp : IPicker
{
    [SerializeField] private float _pickupRange = 1.0f;
    [SerializeField] private LayerMask _pickupLayer;
    private Camera _camera;
    private IInventory _inventory;

    public void InitVariables(Camera camera, IInventory inventory)
    {
        _camera = camera;
        _inventory = inventory;
    }

    public bool TryPickup()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        RaycastHit hit;
        bool success = Physics.Raycast(ray, out hit, _pickupRange, _pickupLayer);
        if (success)
        {
            ItemGameObject itemGameObject;
            if (hit.transform.gameObject.TryGetComponent(out itemGameObject))
            {
                WeaponScriptableObject weapon = itemGameObject.Pickup() as WeaponScriptableObject;
                _inventory.AddItem(weapon);
            }
        }
        Debug.Log("Picup result: " + success);
        return success;
    }
}

interface IPicker
{
    bool TryPickup();
}
