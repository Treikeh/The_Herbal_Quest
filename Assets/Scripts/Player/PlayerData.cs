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

// This struct holds actions that player components can use to comunicate with each other without having to store a references to another player component
// By doing it this way the different player components doesn't have to keep references to other player components which ->
// <- keeps the code decoupled and (arguably) easier to work with
// For example, When the player presses the WASD keys to move the PlayerInput component takes that info and sends it to the ->
// <- PlayerMovement component using the OnMoveInput action
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
