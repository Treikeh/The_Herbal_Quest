using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    public float fadeBuffer = 0.5f;
    public Image fadePanel;
    public GameObject hud;
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject gameOverScreen;


    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadeOutPanel), fadeBuffer);
    }

    // INPUTS //

    public void OnPause()
    {
        if (GameManager.currenctGameState == GameManager.GameStates.Running)
        {
            ShowMouseCursor();
            hud.SetActive(false);
            pauseMenu.SetActive(true);
            GameManager.currenctGameState = GameManager.GameStates.Paused;
            Time.timeScale = 0f;
        }

        else if (GameManager.currenctGameState == GameManager.GameStates.Paused)
        {
            HideMouseCursor();
            hud.SetActive(true);
            pauseMenu.SetActive(false);
            GameManager.currenctGameState = GameManager.GameStates.Running;
            Time.timeScale = 1f;
        }
    }

    public void OnNextLevelButtonPressed()
    {
        StartCoroutine(NextLevelFade());
    }

    public void OnRestartButtonPressed()
    {
        StartCoroutine(RestartLevelFade());
    }

    public void OnMainMenuButtonPressed()
    {
        StartCoroutine(MainMenuFade());
    }

    public void OnQuitButtonPressed()
    {
        StartCoroutine(QuitGameFade());
    }

    //

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

    // FADE INN AND FADE OUT //

    private void FadeOutPanel()
    {
        fadePanel.color = Color.black;
        fadePanel.CrossFadeAlpha(0f, fadeDuration, true);
    }

    private void FadeInnPanel()
    {
        // CrossFadeAlpha does not work without this work around
        fadePanel.CrossFadeAlpha(0f, 0f, true);
        fadePanel.CrossFadeAlpha(1f, fadeDuration, true);
    }

    private IEnumerator NextLevelFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.LoadNextBuildScene();
    }

    private IEnumerator RestartLevelFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.ReloadScene();
    }

    private IEnumerator MainMenuFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.LoadSceneByName("MainMenu");
    }

    private IEnumerator QuitGameFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        Application.Quit();
    }
}
