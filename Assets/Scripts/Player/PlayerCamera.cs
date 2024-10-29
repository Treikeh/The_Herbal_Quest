using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float Sensitivity = 10f;
    [SerializeField] float RotationLimit = 89f;
    [SerializeField] private Transform Orientation;
    [SerializeField] private Transform Head;

    private Vector2 _lookInput;
    private Vector2 _rotation;


    private void Start()
    {
        // Hide mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Camera _rotation
        _rotation.x = _lookInput.x * (Sensitivity * Time.deltaTime);
        _rotation.y -= _lookInput.y * (Sensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        _rotation.y = Mathf.Clamp(_rotation.y, -RotationLimit, RotationLimit);
        // Convert _rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(_rotation.y, Vector3.right);

        // Apply _rotation
        Orientation.Rotate(Vector3.up * _rotation.x);
        Head.localRotation = yQuat;
    }

    public void OnLook(InputValue value)
    {
        _lookInput = value.Get<Vector2>();
    }
}
