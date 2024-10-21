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

    public PlayerData playerData;

    public Image crosshairImage;

    public Sprite crosshiarSprite;
    public Sprite attackIndicator;


    private void Update()
    {
        interactPrompt.text = playerData.interactPrompt;
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
}
