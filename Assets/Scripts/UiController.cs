using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    // Text object that displays the player interact prompt
    [SerializeField] private TMP_Text interactPrompt;

    // Subscribe to events
    void OnEnable()
    {
        // Not the biggest fan of having a reference to the PlayerInteract class, but i don't know of a better way of doing this
        PlayerInteract.UpdateInteractPrompt += OnUpdateInteractPrompt;
    }

    // Unsubscribe to events
    private void OnDisable()
    {
        PlayerInteract.UpdateInteractPrompt -= OnUpdateInteractPrompt;
    }

    private void Start()
    {
        // Clear interact prompt
        OnUpdateInteractPrompt("");
    }

    // What to do when the interact prompt needs to be updated
    void OnUpdateInteractPrompt(string new_prompt)
    {
        interactPrompt.text = new_prompt;
    }
}
