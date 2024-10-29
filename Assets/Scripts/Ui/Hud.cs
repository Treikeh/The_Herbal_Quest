using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private TMP_Text interactText;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private Sprite normalCrosshair;
    [SerializeField] private Sprite attackCrosshair;


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
        interactText.text = "";
        dialogText.text = "";
        crosshairImage.sprite = normalCrosshair;
    }

    private void OnUpdateInteractPrompt(string prompt)
    {
        interactText.text = prompt;
    }

    private void OnUpdateCrosshair(bool attackIcon)
    {
        if (attackIcon)
        {
            crosshairImage.sprite = attackCrosshair;
        }
        else
        {
            crosshairImage.sprite = normalCrosshair;
        }
    }

    private void OnUpdateObjectiveText(string text)
    {
        objectiveText.text = text;
    }

    private void OnUpdateDialogText(string text, float duration)
    {
        dialogText.text = text;
        // Remove text after a delay
        // Make sure the text 
    }
}
