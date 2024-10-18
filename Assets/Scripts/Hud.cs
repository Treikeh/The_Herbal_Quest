using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public TMP_Text interactPrompt;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInteract.interactPromptUpdated += OnInteractPromptUpdated;
    }

    private void OnDisable()
    {
        PlayerInteract.interactPromptUpdated -= OnInteractPromptUpdated;
    }

    void OnInteractPromptUpdated(string new_prompt)
    {
        interactPrompt.text = new_prompt;
    }
}
