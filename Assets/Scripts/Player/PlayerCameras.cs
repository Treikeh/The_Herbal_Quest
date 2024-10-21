using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameras : MonoBehaviour
{
    // How far up and down the camera can be rotated
    public float rotationLimit = 89f;
    // Object to rotate camera left and right
    [SerializeField] private Transform cameraYaw;
    // Object to rotate camera up and down
    [SerializeField] private Transform cameraPitch;

    // Vector to track rotation
    private Vector2 rotation;


    void Update()
    {
        // Rotate camera left and right
        cameraYaw.Rotate(Vector3.up * rotation.x);

        // Rotate camera up and down
        // Convert rotation.y to Quaternion
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.right);
        // Set camera pitch
        cameraPitch.localRotation = yQuat;
    }

    public void UpdateLookInput(Vector2 lookInput)
    {
        rotation.x = lookInput.x;
        rotation.y -= lookInput.y;
        // Limit how far up and down the camera can rotate
        rotation.y = Mathf.Clamp(rotation.y, -rotationLimit, rotationLimit);
    }
}
