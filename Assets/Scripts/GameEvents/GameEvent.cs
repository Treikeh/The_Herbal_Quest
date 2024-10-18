using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

// This combined with the GameEventListener component can be used for global events like when the player dies or when restarting a level
public class GameEvent : ScriptableObject
{
    // All objects that are observing this event
    private List<GameEventListener> listeners = new List<GameEventListener>();

    // Add new object to list of observers
    public void RegisterListeners(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    // Remove object from list of observers
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Trigger()
    {
        Debug.Log("GameEvent " + name + " Triggered");
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered();
        }
    }
}
