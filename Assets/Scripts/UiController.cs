using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    // Text object that displays the player interact prompt
    [SerializeField] private TMP_Text interactPrompt;
    [SerializeField] private GameObject winScreen;

    void OnEnable()
    {
        // Subscribe to events
        GameManager.GameWon += OnGameWon;
        PlayerInteract.UpdateInteractPrompt += OnUpdateInteractPrompt;
    }

    private void OnDisable()
    {
        // Unsubscribe to events
        GameManager.GameWon -= OnGameWon;
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

    // What this script does when the player has won
    void OnGameWon()
    {
        winScreen.SetActive(true);
    }
}
