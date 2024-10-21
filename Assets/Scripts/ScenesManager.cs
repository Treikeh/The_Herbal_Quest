using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is responsible for changing and loading scenes

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance {get; private set;}

    private void Awake()
    {
        // Make this gameObject the ScenesManager singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If a ScenesManager already exists destroy this gameObject to avoid duplicates
            Destroy(gameObject);
            return;
        }
    }


    public void LoadSpecificLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
