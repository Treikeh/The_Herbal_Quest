using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is used when the player needs to interact with an object in the world

public class Interactable : MonoBehaviour, IInteract
{
    // Message to display when this object can be interacted with
    public String Prompt;

    // Set the interface prompt to be the same as the component prompt an idicator on what button to press
    public string interactPrompt => Prompt + "\n" + "[E]";

    // Events to trigger when object is interacted with
    public UnityEvent onInteracted;

    // Function to trigger the onInteracted UnityEvent
    public void Interact()
    {
        onInteracted.Invoke();
    }
}