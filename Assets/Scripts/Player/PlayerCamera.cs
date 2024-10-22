using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public PlayerData playerData;

    // Object to rotate camera left and right
    public Transform cameraYaw;
    // Object to rotate camera up and down
    public Transform cameraPitch;

    // Vector to track rotation
    private Vector2 rotation;


    private void Start()
    {
        playerData.cameraTransform = cameraPitch;
    }

    public void OnLook(InputValue inputValue)
    {
        Vector2 lookInput = inputValue.Get<Vector2>();

        rotation.x = lookInput.x * (playerData.cameraSensitivity * Time.deltaTime);
        rotation.y -= lookInput.y * (playerData.cameraSensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        rotation.y = Mathf.Clamp(rotation.y, -playerData.cameraRotationLimit, playerData.cameraRotationLimit);
        // Convert rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.right);

        // Apply rotation
        cameraYaw.Rotate(Vector3.up * rotation.x);
        cameraPitch.localRotation = yQuat;
    }
}
