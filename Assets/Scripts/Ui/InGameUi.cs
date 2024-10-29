using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private float FadeDuration = 0.5f;
    [SerializeField] private float FadeBuffer = 0.5f;
    [SerializeField] private Image FadePanel;
    [SerializeField] private GameObject Hud;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject GameOverScreen;


    private void Start()
    {
        FadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadeOutPanel), FadeBuffer);
    }

    // INPUTS //

    public void OnPause()
    {
        if (GameManager.currenctGameState == GameManager.GameStates.Running)
        {
            ShowMouseCursor();
            Hud.SetActive(false);
            PauseMenu.SetActive(true);
            GameManager.currenctGameState = GameManager.GameStates.Paused;
            Time.timeScale = 0f;
        }

        else if (GameManager.currenctGameState == GameManager.GameStates.Paused)
        {
            HideMouseCursor();
            Hud.SetActive(true);
            PauseMenu.SetActive(false);
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
        FadePanel.color = Color.black;
        FadePanel.CrossFadeAlpha(0f, FadeDuration, true);
    }

    private void FadeInnPanel()
    {
        // CrossFadeAlpha does not work without this work around
        FadePanel.CrossFadeAlpha(0f, 0f, true);
        FadePanel.CrossFadeAlpha(1f, FadeDuration, true);
    }

    private IEnumerator NextLevelFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(FadeDuration + FadeBuffer);
        SceneLoader.LoadNextBuildScene();
    }

    private IEnumerator RestartLevelFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(FadeDuration + FadeBuffer);
        SceneLoader.ReloadScene();
    }

    private IEnumerator MainMenuFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(FadeDuration + FadeBuffer);
        SceneLoader.LoadSceneByName("MainMenu");
    }

    private IEnumerator QuitGameFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(FadeDuration + FadeBuffer);
        Application.Quit();
    }
}
