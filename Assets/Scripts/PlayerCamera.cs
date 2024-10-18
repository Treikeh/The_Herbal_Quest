using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraSensitivity = 4f;
    public float rotationLimit = 89f;
    public Transform cameraTransform;
    private Vector2 rotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        // Capture the rotation of the player when the game starts.
        // This is necessary since the player rotation is set directly in the update function
        // And without it the player rotation would be reset during the first frame of the game
        rotation.x = transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate player left and right
        rotation.x += Input.GetAxis("Mouse X") * (cameraSensitivity * Time.deltaTime);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);

        transform.localRotation = xQuat;

        // Rotate camera up and down
        rotation.y -= Input.GetAxis("Mouse Y") * (cameraSensitivity * Time.deltaTime);
        // Limit how far up and down the camera can rotate
        rotation.y = Mathf.Clamp(rotation.y, -rotationLimit, rotationLimit);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.right);

        cameraTransform.localRotation = yQuat;
    }
}
