using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeBuffer = 0.5f;
    [SerializeField] private Image fadePanel;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;


    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadePanelOut), fadeBuffer);
    }

    // INPUTS //

    public void OnPause()
    {
        if (GameManager.CurrenctGameState == GameManager.GameStates.Running)
        {
            ShowMouseCursor();
            hud.SetActive(false);
            pauseMenu.SetActive(true);
            GameManager.CurrenctGameState = GameManager.GameStates.Paused;
            Time.timeScale = 0f;
        }

        else if (GameManager.CurrenctGameState == GameManager.GameStates.Paused)
        {
            HideMouseCursor();
            hud.SetActive(true);
            pauseMenu.SetActive(false);
            GameManager.CurrenctGameState = GameManager.GameStates.Running;
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

    public void OnCheckpointButtonPressed()
    {
        StartCoroutine(CheckpointFade());
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

    private void FadePanelOut()
    {
        fadePanel.color = Color.black;
        fadePanel.CrossFadeAlpha(0f, fadeDuration, true);
    }

    private void FadePanelInn()
    {
        // CrossFadeAlpha does not work without this work around
        fadePanel.CrossFadeAlpha(0f, 0f, true);
        fadePanel.CrossFadeAlpha(1f, fadeDuration, true);
    }

    private IEnumerator NextLevelFade()
    {
        FadePanelInn();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.LoadNextBuildScene();
    }

    private IEnumerator RestartLevelFade()
    {
        FadePanelInn();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.ReloadScene();
    }

    private IEnumerator CheckpointFade()
    {
        FadePanelInn();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeDuration);
        //
        HideMouseCursor();
        gameOverScreen.SetActive(false);
        hud.SetActive(true);
        GameManager.ReloadCheckpoint();
        //
        FadePanelOut();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
    }

    private IEnumerator MainMenuFade()
    {
        FadePanelInn();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.LoadSceneByName("MainMenu");
    }

    private IEnumerator QuitGameFade()
    {
        FadePanelInn();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        Application.Quit();
    }
}
