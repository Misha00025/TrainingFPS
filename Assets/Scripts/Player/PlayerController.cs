using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMover _mover;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private Inventory _inventory;
    
    [Header("Inputs")]
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("References")]
    private Animator anim;
    private CharacterController _controller;
    
    public PlayerStats Stats => _stats;
    public Inventory Inventory => _inventory;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void Update()
    {
        HandleMovement();
        HandleInput();
        HandleAnimations();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            Stats.TakeDamage(5);
        }
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0 , moveZ);
        moveDirection = moveDirection.normalized;

        _mover.SetRuning(Input.GetKey(_runKey));
        _mover.Move(moveDirection, Input.GetKeyDown(_jumpKey));
    }

    private void HandleAnimations()
    {
        if(_mover.Direction == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f, 0.2f, Time.deltaTime);
        }
        else if(!Input.GetKey(_runKey)) 
        {
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
        }
    }

    private void GetReferences()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void InitVariables()
    {
        _mover.InitVariables(_controller);
        _stats.InitVariables();
        _inventory.InitVariables();
    }
}
