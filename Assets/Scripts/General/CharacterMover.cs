using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class CharacterMover : IMover
{
    [SerializeField] private float _walkSpeed = 1.0f;
    [SerializeField] private float _runSpeed = 1.5f;
    [SerializeField] private float _jumpForce = 1;
    [SerializeField] private float _gravity = -10;

    private float _moveSpeed;
    private bool _isGrounded = false;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;

    private CharacterController _controller;

    public Vector3 Direction => _moveDirection;

    public void InitVariables(CharacterController controller)
    {
        _controller = controller;
        _moveSpeed = _walkSpeed;
    }

    public void Move(Vector3 direction, bool jump)
    {
        _moveDirection = _controller.transform.TransformDirection(direction);
        Vector3 velocity = _moveDirection * _moveSpeed;

        _isGrounded = _controller.isGrounded;
        if (_isGrounded)
        {
            _velocity.y = _gravity * Time.deltaTime;
            if (jump)
                _velocity.y = _jumpForce;
        }
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
        velocity.y = _velocity.y;
        _velocity = velocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void SetRuning(bool run)
    {
        if (run)
            _moveSpeed = _runSpeed;
        else
            _moveSpeed = _walkSpeed;
    }
}

public interface IMover
{
    void Move(Vector3 direction, bool jump);

    void SetRuning(bool run);
}