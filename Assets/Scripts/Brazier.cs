using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : MonoBehaviour
{
    [SerializeField] private bool startLit;
    [SerializeField] private GameEvent lightTorchEvent;
    [SerializeField] private Light flameLight;

    private bool isLit;


    private void Start()
    {
        if (startLit)
        {
            isLit = true;
            flameLight.enabled = true;
        }
        else
        {
            isLit = false;
            flameLight.enabled = false;
        }
    }

    public void RegisterHit(bool onFire, Transform hitterTransform)
    {
        // Light brazier
        if (onFire && !isLit)
        {
            LightBrazier();
            GameManager.CheckpointPosition = hitterTransform.position;
        }
        // Light torch
        else if (!onFire && isLit)
        {
            lightTorchEvent.TriggerEvent();
        }
    }

    private void LightBrazier()
    {
        isLit = true;
        flameLight.enabled = true;
    }
}
