using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public PlayerEvents Events;
    public float cameraSensitivity = 10f;
}

// This struct holds helper actions that help with keeping player components decoupled
public struct PlayerEvents
{
    // Movement actions
    public Action<Vector2> OnMoveInput;
    public Action OnJumpInput;

    // Camera Actions
    public Action<Vector2> OnLookInput;

    // Interactions Actions
    public Action OnInteraction;
    public Action OnAttacking;
}
