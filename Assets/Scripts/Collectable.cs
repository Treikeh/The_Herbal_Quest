using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // ACtionEvent to trigger when a collectable has been picked up
    public static event Action OnPickUp;

    public void PickUpCollectable()
    {
        // Debug with collectable that has been picked up
        Debug.Log(name + " have been picked up");
        // Trigger the pick up event
        OnPickUp?.Invoke();
        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
