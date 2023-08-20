using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private static PlayerInput _instance;
    public static PlayerInput Instance => _instance;

    public UnityEvent<ActionStatus, KeyCode> KeyPressed { get; private set; } = new UnityEvent<ActionStatus, KeyCode>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        
    }
}

public enum PlayerAction
{
    None,
    Move,
    LookAround
}

public enum ActionStatus
{
    None,
    Down,
    Up,
    Hold
}
