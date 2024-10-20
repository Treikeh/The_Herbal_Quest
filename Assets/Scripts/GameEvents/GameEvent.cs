using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

// When combined with the GameEventObserver this asset can be used to create and trigger global game events
// For example, disabling player input and showing the game over screen when the player is kileld

public class GameEvent : ScriptableObject
{
    // All objects that are observing this event
    private List<GameEventObserver> observers = new List<GameEventObserver>();

    // Add new object to list of observers
    public void AddObserver(GameEventObserver observer)
    {
        observers.Add(observer);
    }

    // Remove object from list of observers
    public void RemoveObserver(GameEventObserver observer)
    {
        observers.Remove(observer);
    }

    // Call this method when you want this GameEvent to be triggered
    public void TriggerEvent()
    {
        // Debug log to see which and when a event is triggered.
        Debug.Log("GameEvent " + name + " Triggered");
        // When this event is triggered, go through every observer it has and activate their response
        // The observers list is read one at the time from back to front.
        //NOTE: To make this code easier to read it could be a good idea to change the name of variable "i" to something more descriptive
        for (int i = observers.Count -1; i >= 0; i--)
        {
            observers[i].RespondToEvent();
        }
    }
}
