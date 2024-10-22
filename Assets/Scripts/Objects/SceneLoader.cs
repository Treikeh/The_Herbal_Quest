using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is probably the biggest sin i have commited in this code base, but it works better than a SceneManager gameObject singleton

[CreateAssetMenu]
public class SceneLoader : ScriptableObject
{
    public BoolAsset isGamePaused;

    public void LoadSpecificScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.LogWarning("Scene " + sceneName + " Not found!!");
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
            ResetPause();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResetPause();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetPause();
    }

    public void LoadNextBuildScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ResetPause();
    }

    // Rest pause when switching scenes
    private void ResetPause()
    {
        isGamePaused.value = false;
        Time.timeScale = 1f;
    }
}
