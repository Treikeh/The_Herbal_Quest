using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GlobalMethods : ScriptableObject
{
    public BoolAsset isGamePaused;


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

    // Rest pause when switching scenes
    private void ResetPause()
    {
        isGamePaused.value = false;
        Time.timeScale = 1f;
    }
}
