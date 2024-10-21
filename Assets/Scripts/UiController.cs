using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    // Text to display how many plants have been picked up
    [SerializeField] private TMP_Text objectiveText;
    // Text object that displays the player interact prompt
    [SerializeField] private TMP_Text interactPrompt;

    public Image crosshairImage;

    public Sprite crosshiarSprite;
    public Sprite attackIndicator;

    // Subscribe to events
    private void OnEnable()
    {
        // Not the biggest fan of having a reference to the PlayerInteract class, but i don't know of a better way of doing this
        PlayerInteract.UpdateInteractPrompt += OnUpdateInteractPrompt;
        PlayerAttck.UpdateCrosshair += UpdateCrosshair;
    }

    // Unsubscribe to events
    private void OnDisable()
    {
        PlayerInteract.UpdateInteractPrompt -= OnUpdateInteractPrompt;
        PlayerAttck.UpdateCrosshair -= UpdateCrosshair;
    }


    private void Start()
    {
        // Clear interact prompt
        OnUpdateInteractPrompt("");
    }

    public void OnPlantPickedUp(int havePickedUp, int toPickUp)
    {
        objectiveText.text = "Plants to pick up " + havePickedUp.ToString() + " / " + toPickUp.ToString();
    }

    private void OnUpdateInteractPrompt(string new_prompt)
    {
        interactPrompt.text = new_prompt;
    }

    private void UpdateCrosshair(bool canAttack)
    {
        if (canAttack)
        {
            // Change crosshiar to attack icon
            crosshairImage.sprite = attackIndicator;
        }
        else
        {
            // Reset crosshair
            crosshairImage.sprite = crosshiarSprite;
        }
    }
}
