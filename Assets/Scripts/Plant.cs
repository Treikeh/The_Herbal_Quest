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
        CheckpointManager.SaveCheckpoint();
        gameObject.SetActive(false);
    }
}
