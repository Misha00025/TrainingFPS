using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private float _pickupRange = 1.0f;
    [SerializeField] private LayerMask _pickupLayer;
    private Camera _camera;
    private Inventory _inventory;

    [SerializeField] private KeyCode _pickUpCode = KeyCode.E;


    private void Start()
    {
        InitReferences();
    }

    private void Update()
    {
        InputHandler();
    }

    private void InitReferences()
    {
        _camera = GetComponentInChildren<Camera>();
        _inventory = GetComponent<Inventory>();
    }

    private void InputHandler()
    {
        if (Input.GetKey(_pickUpCode))
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _pickupRange, _pickupLayer)) 
            {
                ItemGameObject itemGameObject;
                if (hit.transform.gameObject.TryGetComponent(out itemGameObject))
                {
                    Weapon weapon = itemGameObject.Pickup() as Weapon;
                    _inventory.AddItem(weapon);
                }
            }
            else
            {
                Debug.Log("Pickup hit not founded");
            }
        }
    }
}
