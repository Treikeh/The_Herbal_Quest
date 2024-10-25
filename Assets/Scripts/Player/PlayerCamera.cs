using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivity = 10f;
    public float rotationLimit = 89f;
    public Transform orientation;
    public Transform head;

    private Vector2 lookInput;
    private Vector2 rotation;


    private void Start()
    {
        // Hide mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Camera rotation
        rotation.x = lookInput.x * (sensitivity * Time.deltaTime);
        rotation.y -= lookInput.y * (sensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        rotation.y = Mathf.Clamp(rotation.y, -rotationLimit, rotationLimit);
        // Convert rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.right);

        // Apply rotation
        orientation.Rotate(Vector3.up * rotation.x);
        head.localRotation = yQuat;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
