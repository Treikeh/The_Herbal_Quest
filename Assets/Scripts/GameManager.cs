using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    // How many plants to collect
    [SerializeField] private int plantsToPickUp;
    
    // Event to trigger when all plants have been picked up
    [SerializeField] private GameEvent AllPlantsPickedUpEvent;

    void Start() {
        // Limit frame rate
        Application.targetFrameRate = 90;
    }

    void Update()
    {
        // Reload scene when pressing the R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Check if all the plants have been picked up
    public void PlantPickedUp()
    {
        plantsToPickUp --;
        if (plantsToPickUp <= 0)
        {
            AllPlantsPickedUpEvent.Trigger();
        }
    }
}