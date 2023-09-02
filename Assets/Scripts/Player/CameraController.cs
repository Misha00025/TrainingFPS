using System;
using UnityEngine;

[Serializable]
public class CameraController : ICameraMover
{
    [SerializeField, Range(0, 90)] private int _maxAngle = 90;
    [SerializeField, Range(-90, 0)] private int _minAngle = -90;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _bodyTransform;

    private float _xRotation;

    public void InitVariables()
    {
        LockCursor();
    }

    public void RotateCamera(float mouseX, float mouseY)
    {
        if (_headTransform == null) throw new System.Exception("Не задан Transform для головы");
        if (_bodyTransform == null) throw new System.Exception("Не задан Transform для тела");

        _xRotation -= mouseY;
        _xRotation =  Mathf.Clamp(_xRotation, _minAngle, _maxAngle);

        _headTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _bodyTransform.Rotate(new Vector3(0, mouseX, 0)); 
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState -= CursorLockMode.None;
    }
}

interface ICameraMover
{
    void RotateCamera(float mouseX, float mouseY);
    void LockCursor();
    void UnlockCursor();
}
