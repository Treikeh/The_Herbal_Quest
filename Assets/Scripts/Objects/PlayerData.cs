using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    // Camera
    public float cameraSensitivity = 10f;
    public float cameraRotationLimit = 89f;
    [HideInInspector]
    public Transform cameraTransform;
    // Movement
    public float walkSpeed = 5f;
    public float acceleration = 10f;
    public float jumpForce = 5f;
    // Ground check
    public bool isGrounded;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    // Interaction
    public float interactDistance = 2f; // Is also used for Attack distance
    public float attackDamage = 2f;
}