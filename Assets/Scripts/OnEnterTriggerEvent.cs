using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterTriggerEvent : MonoBehaviour
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
