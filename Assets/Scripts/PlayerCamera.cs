using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float cameraSensitivity = 4f;
    public float rotationLimit = 89f;
    public Transform cameraTransform;
    private float y_rotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate player left and right
        float x_rotation = Input.GetAxis("Mouse X") * cameraSensitivity;
        transform.Rotate(Vector3.up, x_rotation);

        // Rotate camera up and down
        y_rotation -= Input.GetAxis("Mouse Y") * cameraSensitivity;
        y_rotation = Mathf.Clamp(y_rotation, -rotationLimit, rotationLimit);
        var yQuat = Quaternion.AngleAxis(y_rotation, Vector3.right);

        cameraTransform.localRotation = yQuat;
    }
}
