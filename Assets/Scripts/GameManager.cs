using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    private void Start() {
        // Limit frame rate
        Application.targetFrameRate = 90;
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

    public void HerbPickedUp(String herbName)
    {
        Debug.Log(herbName);
    }
}