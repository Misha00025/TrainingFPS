using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 1.0f;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _bodyTransform;

    private float _xRotation;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        
        HandleMouseLook();
    }

    private void HandleMouseLook()
    {
        if (_headTransform == null) throw new System.Exception("Не задан Transform для головы");
        if (_bodyTransform == null) throw new System.Exception("Не задан Transform для тела");

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation =  Mathf.Clamp(_xRotation, -90, 90);

        _headTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _bodyTransform.Rotate(new Vector3(0, mouseX, 0)); 
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState -= CursorLockMode.None;
    }

}
