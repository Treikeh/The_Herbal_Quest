using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    public SharedData sharedData;

    public TMP_Text objectiveText;
    public TMP_Text interactPromptText;
    public Image crosshairImage;
    public Sprite crosshiarSprite;
    public Sprite attackIndicator;

    public void Update()
    {
        objectiveText.text = sharedData.objectiveText;
        interactPromptText.text = sharedData.interactPrompt;
        // Attack icon
        if (sharedData.displayAttackIcon)
            { crosshairImage.sprite = attackIndicator; }
        else
            { crosshairImage.sprite = crosshiarSprite; }
    }
}
