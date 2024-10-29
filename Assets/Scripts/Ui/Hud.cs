using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    [SerializeField] private TMP_Text ObjectiveText;
    [SerializeField] private TMP_Text InteractText;
    [SerializeField] private TMP_Text DialogText;
    [SerializeField] private Image CrosshairImage;

    [SerializeField] private Sprite NormalCrosshair;
    [SerializeField] private Sprite AttackCrosshair;


    private void OnEnable()
    {
        GameManager.UpdateObjectiveText += OnUpdateObjectiveText;
        PlayerInteract.UpdateInteractPrompt += OnUpdateInteractPrompt;
        DialogTrigger.UpdateDialogText += OnUpdateDialogText;
        PlayerAttacking.UpdateCrosshair += OnUpdateCrosshair;
    }

    private void OnDisable()
    {
        GameManager.UpdateObjectiveText -= OnUpdateObjectiveText;
        PlayerInteract.UpdateInteractPrompt -= OnUpdateInteractPrompt;
        DialogTrigger.UpdateDialogText -= OnUpdateDialogText;
        PlayerAttacking.UpdateCrosshair -= OnUpdateCrosshair;
    }


    private void Start()
    {
        InteractText.text = "";
        DialogText.text = "";
        CrosshairImage.sprite = NormalCrosshair;
    }

    private void OnUpdateInteractPrompt(string prompt)
    {
        InteractText.text = prompt;
    }

    private void OnUpdateCrosshair(bool attackIcon)
    {
        if (attackIcon)
        {
            CrosshairImage.sprite = AttackCrosshair;
        }
        else
        {
            CrosshairImage.sprite = NormalCrosshair;
        }
    }

    private void OnUpdateObjectiveText(string text)
    {
        ObjectiveText.text = text;
    }

    private void OnUpdateDialogText(string text, float duration)
    {
        DialogText.text = text;
        // Remove text after a delay
        // Make sure the text 
    }
}
