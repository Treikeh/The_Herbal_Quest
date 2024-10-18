using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // How fast to rotate camera
    public float cameraSensitivity = 4f;
    // How far up and down the camera can be rotated
    public float rotationLimit = 89f;
    // How far up and down the camera has been rotated
    private float y_rotation;
    // Object to rotate camera left and right
    [SerializeField] private Transform cameraYaw;
    // Object to rotate camera up and down
    [SerializeField] private Transform cameraPitch;

    // Subscribe to events
    void OnEnable()
    {
        GameManager.GameWon += DisableCamera;
    }

    // Unsubscribe to events
    private void OnDisable()
    {
        GameManager.GameWon -= DisableCamera;
    }

    private void Start()
    {
        // Lock mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        // Rotate camera left and right
        float x_rotation = Input.GetAxis("Mouse X") * (cameraSensitivity * Time.deltaTime);
        cameraYaw.Rotate(Vector3.up * x_rotation);

        // Rotate camera up and down
        y_rotation -= Input.GetAxis("Mouse Y") * (cameraSensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        y_rotation = Mathf.Clamp(y_rotation, -rotationLimit, rotationLimit);
        // Convert y_rotation to Quaternion
        var yQuat = Quaternion.AngleAxis(y_rotation, Vector3.right);
        // Set camera pitch
        cameraPitch.localRotation = yQuat;
    }

    // Disable camera controls
    private void DisableCamera()
    {
        // Show mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        enabled = false;
    }
}
