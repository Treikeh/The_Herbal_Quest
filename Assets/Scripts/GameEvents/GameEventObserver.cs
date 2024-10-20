using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is responsible for observing GameEvents and activating responses when the event is triggered,

public class GameEventObserver : MonoBehaviour
{
    // GameEvent to observe
    public GameEvent Event;

    // You can connect this to many different objects in the same way you would with Ui and they will all activate when the Event is triggered
    public UnityEvent Response;

    // Add this component to the Event observers list
    private void OnEnable()
    {
        Event.AddObserver(this);
    }

    // Remove this component from the Event observers list
    private void OnDisable()
    {
        Event.RemoveObserver(this);
    }

    // Activate Response when the Event is triggered
    public void RespondToEvent()
    {
        // Debug log to see when and which GameEventListner that responds to a GameEvent
        Debug.Log("GameEventListener " + name + " Responded");
        Response.Invoke();
    }
}
