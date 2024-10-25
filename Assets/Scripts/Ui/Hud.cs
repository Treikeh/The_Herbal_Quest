using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    public PlayerData playerData;
    public StringAsset objectiveString;

    public TMP_Text objectiveText;
    public TMP_Text interactPromptText;
    public Image crosshairImage;
    public Sprite crosshiarSprite;
    public Sprite attackIndicator;

    public void Update()
    {
        objectiveText.text = objectiveString.value;
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
}
