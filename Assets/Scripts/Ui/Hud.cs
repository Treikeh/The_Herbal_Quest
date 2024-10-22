using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    public PlayerData playerData;

    public TMP_Text objectiveText;
    public TMP_Text interactPromptText;
    public Image crosshairImage;
    public Sprite crosshiarSprite;
    public Sprite attackIndicator;

    public void Update()
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
        objectiveText.text = "You have picked up " + havePickedUp.ToString() + " / " + toPickUp.ToString() + " plants";
    }
}
