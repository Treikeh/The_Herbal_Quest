using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    // Event to trigger when the player enters this trigger and wins
    [SerializeField] private GameEvent GameWonEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Check to make it's a player
        if (other.tag == "Player")
        {
            GameWonEvent.TriggerEvent();
        }
    }
}
