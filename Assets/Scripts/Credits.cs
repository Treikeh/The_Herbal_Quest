using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject mainCredits;
    [SerializeField] private GameObject assetsCredits;

    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float fadeBuffer = 0.5f;
    [SerializeField] private Image fadePanel;

    public void OnAssetCreditsButtonPressed()
    {
        mainCredits.SetActive(false);
        assetsCredits.SetActive(true);
    }

    public void OnMainCreditsButtonPressed()
    {
        mainCredits.SetActive(true);
        assetsCredits.SetActive(false);
    }

    public void OnMainMenuButtonPressed()
    {
        StartCoroutine(MainMenuFade());
    }

    private void FadePanelInn()
    {
        // CrossFadeAlpha does not work without this work around
        fadePanel.CrossFadeAlpha(0f, 0f, true);
        fadePanel.CrossFadeAlpha(1f, fadeDuration, true);
    }

    private IEnumerator MainMenuFade()
    {
        FadePanelInn();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
        SceneLoader.LoadSceneByName("MainMenu");
    }
}
