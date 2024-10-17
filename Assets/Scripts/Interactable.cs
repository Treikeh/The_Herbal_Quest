using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is used when the player needs to interact with an object in the world

public class Interactable : MonoBehaviour
{
    // Message to display when this object can be interacted with
    public String interactPrompt;

    // Events to trigger when object is interacted with
    public UnityEvent onInteracted;

    // Function to trigger the onInteracted UnityEvent
    public void Interact() {
        onInteracted.Invoke();
    }
}
