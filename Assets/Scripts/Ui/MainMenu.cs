using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeBuffer = 0.5f;
    [SerializeField] private Image fadePanel;
    [SerializeField] private GameObject mainMenuMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject introDialog;

    // Intro dialog
    [SerializeField] private List<string> xiyuanDialog;
    [SerializeField] private List<string> lixinDialog;
    [SerializeField] private TMP_Text xiyuanText;
    [SerializeField] private TMP_Text lixinText;
    private bool dialogActive = false;
    private int xiyuanDialogIndex = 0;
    private int lixinDialogIndex = 0;



    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadeOutPanel), fadeBuffer);
    }

    private void Update()
    {
        if (dialogActive)
        {
            // Change dialog lines
        }
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

    public void OnLevelSelectButtonPressed()
    {
        mainMenuMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        mainMenuMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void OnLevelButtonPressed(int level)
    {
        StartCoroutine(LevelButtonsFade(level));
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
        mainMenuMenu.SetActive(false);
        StartCoroutine(DialogStartFade());
    }

    private IEnumerator DialogStartFade()
    {
        FadeOutPanel();
        introDialog.SetActive(true);
        xiyuanText.text = xiyuanDialog[xiyuanDialogIndex];
        lixinText.text = lixinDialog[lixinDialogIndex];
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        dialogActive = true;
    }

    private IEnumerator DialogEndFade()
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

    private IEnumerator LevelButtonsFade(int level)
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        switch (level) {
            case 1:
                SceneLoader.LoadSceneByName("Level 1");
                break;
            case 2:
                SceneLoader.LoadSceneByName("Level 2");
                break;
            case 3:
                SceneLoader.LoadSceneByName("Level 3");
                break;
            default:
                Debug.Log("Enter level number (1 - 3)");
                FadeOutPanel();
                break;
        }
    }
}
