using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public static event Action OnPickUp;

    public void PickUpCollectable()
    {
        // Debug which collectable that has been picked up
        Debug.Log(name + " have been picked up");
        // Trigger the pick up event
        OnPickUp?.Invoke();
        // Disable the GameObject
        gameObject.SetActive(false);
    }
}
