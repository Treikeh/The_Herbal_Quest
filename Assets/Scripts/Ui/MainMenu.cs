using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeBuffer = 0.5f;
    [SerializeField] private Image fadePanel;


    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadeOutPanel), fadeBuffer);
    }

    // INPUTS //

    public void OnPlayButtonPressed()
    {
        StartCoroutine(PlayButtonFade());
    }

    public void OnQuitButtonPressed()
    {
        StartCoroutine(QuitGameFade());
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

    private IEnumerator PlayButtonFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.LoadNextBuildScene();
    }

    private IEnumerator QuitGameFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        Application.Quit();
    }
}
