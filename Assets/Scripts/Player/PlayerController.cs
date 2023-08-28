using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 1.0f;
    [SerializeField] private float _runSpeed = 1.5f;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private float _gravity = -10;
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Animations")]
    private Animator anim;

    private float _moveSpeed;
    private bool _isGrounded = false;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    private CharacterController _controller;    

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void Update()
    {
        HandleGravity();
        HandleJumping();
        HandleRuning();
        HandleMovement();
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
        {
            GetComponent<PlayerStats>().TakeDamage(5);
        }

        HandleAnimations();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0 , moveZ);
        moveDirection = moveDirection.normalized;
        _moveDirection = transform.TransformDirection(moveDirection);

        _controller.Move(_moveDirection * _moveSpeed * Time.deltaTime);
    }

    private void HandleRuning()
    {
        if (Input.GetKeyDown(_runKey))
        {
            _moveSpeed = _runSpeed;
        }
        if (Input.GetKeyUp(_runKey))
        {
            _moveSpeed = _walkSpeed;
        }
    }

    private void HandleAnimations()
    {
        if(_moveDirection == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f, 0.2f, Time.deltaTime);
        }
        else if(_moveDirection!= Vector3.zero && !Input.GetKey(_runKey)) 
        {
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }
        else if (_moveDirection != Vector3.zero && Input.GetKey(_runKey))
        {
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
        }
    }

    private void HandleGravity()
    {  
        _controller.Move(_velocity * Time.deltaTime);      
        _isGrounded = _controller.isGrounded;
        if (_isGrounded)
        {
            _velocity.y = _gravity * 0.1f;
        }        
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }        
    }

    private void HandleJumping()
    {
        if (_isGrounded && Input.GetKeyDown(_jumpKey))
        {
            _velocity.y = _jumpForce;
        }
    }

    private void GetReferences()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void InitVariables()
    {
        _moveSpeed = _walkSpeed;
    }
}
