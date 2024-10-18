using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component when combined with GameEvents can be used for global events like when the player dies or when restarting a level
public class GameEventListener : MonoBehaviour
{
    // GameEvent to observe
    public GameEvent Event;

    // Response to activate when Event is triggered
    public UnityEvent Response;

    private void OnEnable() {
        // Add this objefct as an observer to Event
        Event.RegisterListeners(this);
    }

    private void OnDisable() {
        // Remove this object as an observer to Event
        Event.UnregisterListener(this);
    }

    // Activate Response when the Event is triggered
    public void OnEventTriggered() {
        Response.Invoke();
    }
}
