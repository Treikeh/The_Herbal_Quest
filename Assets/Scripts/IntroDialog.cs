using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialog : MonoBehaviour
{
    [SerializeField] private List<string> xiyuanDialog;
    [SerializeField] private List<string> lixinDialog;
    [SerializeField] private TMP_Text xiyuanText;
    [SerializeField] private TMP_Text lixinText;
    [SerializeField] private Image fadePanel;

    private float fadeDuration = 0.25f;
    private float fadeBuffer = 0.25f;

    private int xiyuanDialogIndex = 0;
    private int lixinDialogIndex = 0;

    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        Invoke(nameof(FadeOutPanel), fadeBuffer);
    }

    private void Update()
    {
        //
    }

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

    private IEnumerator IntroFade()
    {
        FadeInnPanel();
        yield return new WaitForSecondsRealtime(fadeDuration + fadeBuffer);
    }
}
