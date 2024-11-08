using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is used when the player needs to interact with an object in the world

public class Interactable : MonoBehaviour
{
    // Message to display when this object can be interacted with
    [SerializeField] private string prompt;
    public string Prompt
    {
        set {prompt = value;}
        get{ return prompt + "\n" + "[E]";}
    }

    public UnityEvent OnInteracted;


    public void Interact()
    {
        OnInteracted.Invoke();
    }
}