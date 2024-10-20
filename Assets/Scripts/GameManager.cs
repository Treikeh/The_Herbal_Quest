using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    // How many plants the player needs to pcik up before they can exit the level
    [SerializeField] private int toPickUp;
    // How many plants the player has picked up
    private int havePickedUp = 0;
    // Event to trigger when the player picks up a plant
    public static event Action<int, int> WhenPickedUp;
    
    // Event to trigger when all plants have been picked up
    [SerializeField] private GameEvent AllPlantsPickedUpEvent;

    private void Start()
    {
        // Limit frame rate
        Application.targetFrameRate = 90;
        // Reset everything that needs this info like the Ui
        WhenPickedUp?.Invoke(havePickedUp, toPickUp);
    }

    public void PickUpPlant()
    {
        havePickedUp++;
        WhenPickedUp?.Invoke(havePickedUp, toPickUp);
        if (havePickedUp >= toPickUp)
        {
            AllPlantsPickedUpEvent.TriggerEvent();
        }
    }
}