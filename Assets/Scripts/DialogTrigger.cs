using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string dialogText;
    [SerializeField] private float dialogDuration;

    private bool beenTriggred = false;

    public static Action<string, float> UpdateDialogText;


    private void OnTriggerEnter(Collider other)
    {
        if (!beenTriggred)
        {
            beenTriggred = true;
            UpdateDialogText?.Invoke(dialogText, dialogDuration);
        }
    }

    public void ManualTrigger()
    {
        if (!beenTriggred)
        {
            beenTriggred = true;
            UpdateDialogText?.Invoke(dialogText, dialogDuration);
        }
    }
}
