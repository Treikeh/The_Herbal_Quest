using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InGameUi : MonoBehaviour
{
    public PlayerData playerData;
    // Text to display how many plants have been picked up
    [SerializeField] private TMP_Text objectiveText;
    // Text object that displays the player interact prompt
    [SerializeField] private TMP_Text interactPromptText;

    public Image crosshairImage;

    public Sprite crosshiarSprite;
    public Sprite attackIndicator;


    private void Start()
    {
        HideMouseCursor();
    }

    private void Update()
    {
        interactPromptText.text = playerData.interactPrompt;
        if (playerData.displayAttackIcon)
        {
            crosshairImage.sprite = attackIndicator;
        }
        else
        {
            crosshairImage.sprite = crosshiarSprite;
        }
    }

    public void OnPlantPickedUp(int havePickedUp, int toPickUp)
    {
        objectiveText.text = "Plants to pick up " + havePickedUp.ToString() + " / " + toPickUp.ToString();
    }

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
}
