using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GlobalMethods : ScriptableObject
{
    private void OnEnable()
    {
        // Reset values when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Scene loader

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
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextBuildScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

// Other

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Rest pause when switching scenes
        Time.timeScale = 1f;
        GameManager.isGameOver = false;
        GameManager.isGamePaused = false;
    }
}
