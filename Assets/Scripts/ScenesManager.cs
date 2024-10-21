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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // If a ScenesManager already exists destroy this game object
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
