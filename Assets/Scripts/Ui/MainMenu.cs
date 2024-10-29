using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float FadeDuration = 0.5f;
    [SerializeField] private float FadeBuffer = 0.5f;
    
    public Image FadePanel;


    private void Start()
    {
        FadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadeOutPanel), FadeBuffer);
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
        FadePanel.color = Color.black;
        FadePanel.CrossFadeAlpha(0f, FadeDuration, true);
    }

    private void FadeInnPanel()
    {
        // CrossFadeAlpha does not work without this work around
        FadePanel.CrossFadeAlpha(0f, 0f, true);
        FadePanel.CrossFadeAlpha(1f, FadeDuration, true);
    }

    private IEnumerator PlayButtonFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(FadeDuration + FadeBuffer);
        SceneLoader.LoadNextBuildScene();
    }

    private IEnumerator QuitGameFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(FadeDuration + FadeBuffer);
        Application.Quit();
    }
}
