using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is responsible for changing and loading scenes

public class ScenesManager : MonoBehaviour
{
    // ScenesManager Singleton reference variable
    public static ScenesManager Instance {get; private set;}

    private void Awake()
    {
        // If there isn't aleady a ScenesManager make this GameObject the ScenesManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        // If a ScenesManager already exists destroy this game object
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Reload scene when pressing the R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
