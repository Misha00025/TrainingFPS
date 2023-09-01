using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleEquipmentInput();
        HandleInput();
    }

    private void HandleEquipmentInput()
    {
        WeaponStyle weaponStyle = WeaponStyle.Primary;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponStyle = WeaponStyle.Primary;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponStyle = WeaponStyle.Secondary;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponStyle = WeaponStyle.Melee;
        }
    }

    private void HandleMovementInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = moveDirection.normalized;

        _playerController.SetRuning(Input.GetKey(_runKey));
        _playerController.Move(moveDirection, Input.GetKeyDown(_jumpKey));
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _playerController.TakeDamage(5);
        }
    }
}

    
