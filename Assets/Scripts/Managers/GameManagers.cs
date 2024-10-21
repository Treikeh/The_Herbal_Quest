using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManagers : MonoBehaviour
{
    // How many collectables the player need to pick up before they can exit the level
    [SerializeField] private int toPickUp;
    // How many collectables the player has picked up
    private int havePickedUp = 0;

    // Event to trigger when the player picks up a collectable
    public UnityEvent<int, int> OnCollectablePickedUp;
    // Event to trigger when all plants have bee
    public UnityEvent OnAllCollectablesPickedUp;
    public UnityEvent OnLevelEnded;

    // Subscribe to events
    private void OnEnable()
    {
        Collectable.OnPickUp += OnCollectablePickUp;
    }

    // Unsubscribe to events
    private void OnDisable()
    {
        Collectable.OnPickUp -= OnCollectablePickUp;
    }


    private void Start()
    {
        // Limit frame rate
        Application.targetFrameRate = 90;
        // Reset everything that needs this info, like the Ui
        OnCollectablePickedUp.Invoke(havePickedUp, toPickUp);
    }

    public void OnCollectablePickUp()
    {
        havePickedUp++;
        OnCollectablePickedUp.Invoke(havePickedUp, toPickUp);
        if (havePickedUp >= toPickUp)
        {
            OnAllCollectablesPickedUp.Invoke();
        }
    }

    public void EndLevel()
    {
        OnLevelEnded.Invoke();
    }
}