using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    // How many plants to collect
    public int plantsToPickUp;

    public GameEvent gameWon;

    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    private void Start() {
        // Limit frame rate
        Application.targetFrameRate = 90;
        // Lock mouse cursor
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    // Update is called once per frame
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
            gameWon.Trigger();
            // Show mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}