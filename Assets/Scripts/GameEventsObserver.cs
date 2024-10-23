using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is responsible for observing GameEvents and activating responses when the event is triggered,

public class GameEventsObserver : MonoBehaviour
{
    // EVery game event that this component can observe
    public List<GameEventResponse> Events;

    // You can connect this to many different objects in the same way you would with Ui and they will all activate when any Event is triggered
    public UnityEvent AnyResponse;

    // Add this component to the Events observers list
    private void OnEnable()
    {
        foreach (GameEventResponse gameEventResponse in Events)
        {
            gameEventResponse.gameEvent.AddObserver(this);
        }
    }

    // Remove this component from the Events observers list
    private void OnDisable()
    {
        foreach (GameEventResponse gameEventResponse in Events)
        {
            gameEventResponse.gameEvent.RemoveObserver(this);
        }
    }

    // Activate Response when the Event is triggered
    public void RespondToEvent(string eventName)
    {
        // Debug log to see when and which GameEventListner that responds to a GameEvent
        Debug.Log("GameEventListener " + name + " Responded");
        // Get 
        foreach (GameEventResponse gameEventResponse in Events)
        {
            if (gameEventResponse.gameEvent.name == eventName)
            {
                gameEventResponse.response.Invoke();
            }
        }
        AnyResponse.Invoke();
    }
}

[System.Serializable]
public class GameEventResponse
{
    // This is here so that each list element can have its own name
    // I would prefer it to use the name of the gameEvent
    public string title;
    public GameEvent gameEvent;
    public UnityEvent response;
}
