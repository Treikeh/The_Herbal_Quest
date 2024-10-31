using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Use this component when a gameObject needs to trigger a GameEvent entering a trigger

public class OnEnterEventTrigger : MonoBehaviour
{
    [SerializeField] private string objectTag = "Player";

    public UnityEvent response;


    private void OnTriggerEnter(Collider other)
    {
        // Check if tag is matching
        if (other.tag == objectTag)
        {
            response.Invoke();
        }
    }
}
