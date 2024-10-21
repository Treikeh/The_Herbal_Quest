using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WinTrigger : MonoBehaviour
{
    public UnityEvent PlayerEnteredTrigger;

    private void OnTriggerEnter(Collider other)
    {
        // Check to make it's a player
        if (other.tag == "Player")
        {
            PlayerEnteredTrigger.Invoke();
        }
    }
}
