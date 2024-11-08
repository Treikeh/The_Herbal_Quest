using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float sensitivity = 10f;
    [SerializeField] float rotationLimit = 89f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform head;

    private Vector2 lookInput;
    private Vector2 rotation;


    private void Start()
    {
        // Hide mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Camera _rotation
        rotation.x = lookInput.x * (sensitivity * Time.fixedDeltaTime);
        rotation.y -= lookInput.y * (sensitivity * Time.fixedDeltaTime);
        // Limit how far up and down the camera can rotate
        rotation.y = Mathf.Clamp(rotation.y, -rotationLimit, rotationLimit);
        // Convert _rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.right);

        // Apply _rotation
        orientation.Rotate(Vector3.up * rotation.x);
        head.localRotation = yQuat;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
