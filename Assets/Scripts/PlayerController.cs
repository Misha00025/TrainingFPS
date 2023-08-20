using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _walkSpeed = 0.5f;
    [SerializeField] private float _runSpeed = 1.5f;

    private Vector3 _moveDirection = Vector3.zero;

    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0 , moveZ);
        moveDirection = moveDirection.normalized;



        _controller.Move(moveDirection * Time.deltaTime);
    }
}
