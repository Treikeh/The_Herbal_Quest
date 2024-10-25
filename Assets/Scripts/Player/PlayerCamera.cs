using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public PlayerData playerData;
    public float cameraRotationLimit = 89f;
    public Transform cameraYaw;
    public Transform cameraPitch;

    private Vector2 lookInput;
    private Vector2 cameraRotation;


    private void Start()
    {
        // Hide mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Camera rotation
        cameraRotation.x = lookInput.x * (playerData.cameraSensitivity * Time.deltaTime);
        cameraRotation.y -= lookInput.y * (playerData.cameraSensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -cameraRotationLimit, cameraRotationLimit);
        // Convert rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(cameraRotation.y, Vector3.right);

        // Apply rotation
        cameraYaw.Rotate(Vector3.up * cameraRotation.x);
        cameraPitch.localRotation = yQuat;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
}
