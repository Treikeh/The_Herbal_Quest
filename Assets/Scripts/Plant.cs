using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameEvent pickUpEvent;
    [SerializeField] private AudioClip PickUpSound;


    public void OnPickUp()
    {
        pickUpEvent.TriggerEvent();
        AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
        // ! TESTING ONLY
        // Find a better solution to saving the position of the player
        GameManager.CheckpointPosition = GameObject.Find("Player").transform.position;
        gameObject.SetActive(false);
    }
}
