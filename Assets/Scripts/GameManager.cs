using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    // A list of all the plants that the player needs to collect during this level
    public List<String> plantsToCollect = new List<String>();

    // Update is called once per frame
    void Update()
    {
        // Reload scene when pressing the R key
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void HerbPickedUp(String herbName) {
        Debug.Log(herbName);
    }
}
