using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string DialogText;
    [SerializeField] private float DialogDuration;

    private bool _triggred = false;

    public static Action<string, float> UpdateDialogText;


    private void OnTriggerEnter(Collider other)
    {
        if (!_triggred)
        {
            _triggred = true;
            UpdateDialogText?.Invoke(DialogText, DialogDuration);
        }
    }
}
