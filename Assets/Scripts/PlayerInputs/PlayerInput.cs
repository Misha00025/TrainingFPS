using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    [Header("Camera Input")]
    [SerializeField] private float _mouseSensitivity;

    [Header("Movement Input")]
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Equipment Input")]
    [SerializeField] private KeyCode _primarySlotKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode _secondarySlotKey = KeyCode.Alpha2;
    [SerializeField] private KeyCode _meleeSlotKey = KeyCode.Alpha3;

    [Header("Shooting Input")]
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

    [Header("Other Input")]
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    [SerializeField] private KeyCode _takeDamageSelfKey = KeyCode.O;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleCameraInput();
        HandleMovementInput();
        HandleEquipmentInput();
        HandleShootingInput();
        HandleOtherInput();
    }

    private void HandleOtherInput()
    {
        if (Input.GetKey(_interactionKey))
        {
            _playerController.TryPickup();
        }
        if (Input.GetKeyDown(_takeDamageSelfKey))
        {
            _playerController.TakeDamage(5);
        }
    }

    private void HandleShootingInput()
    {
        if (Input.GetKeyDown(_shootKey))
        {
            _playerController.Shoot();
        }
    }

    private void HandleEquipmentInput()
    {
        if (Input.GetKeyDown(_primarySlotKey))
        {
            _playerController.SetCurrentWeapon(WeaponStyle.Primary);
        }
        if (Input.GetKeyDown(_secondarySlotKey))
        {
            _playerController.SetCurrentWeapon(WeaponStyle.Secondary);
        }
        if (Input.GetKeyDown(_meleeSlotKey))
        {
            _playerController.SetCurrentWeapon(WeaponStyle.Melee);
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

    private void HandleCameraInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _playerController.CameraController.RotateCamera(mouseX, mouseY);
    }
}

    
