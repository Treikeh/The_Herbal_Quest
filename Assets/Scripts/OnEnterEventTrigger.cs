using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Use this component when a gameObject needs to trigger a GameEvent entering a trigger

public class OnEnterEventTrigger : MonoBehaviour
{
    public string objectTag;
    public UnityEvent response;


    private void OnTriggerEnter(Collider other)
    {
        // Check to make it's a player
        if (other.tag == objectTag)
        {
            response.Invoke();
        }
    }
}