using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameEvent pickUpEvent;
    [SerializeField] private AudioClip PickUpSound;


    public void OnPickUp(Transform interacterTransform)
    {
        pickUpEvent.TriggerEvent();
        AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
        gameObject.SetActive(false);
        // ! TESTING ONLY
        // TODO Find a better solution to saving the position of the player
        GameManager.CheckpointPosition = interacterTransform.position;
    }
}
