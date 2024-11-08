using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadSceneByName(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.LogWarning("Scene " + sceneName + " Not found!!");
            return;
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadNextBuildScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
