using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brazier : MonoBehaviour, IHitable
{
    [SerializeField] private bool startLit;
    [SerializeField] private GameEvent lightTorchEvent;
    [SerializeField] private GameObject flameEffect;

    private bool isLit;


    private void Start()
    {
        if (startLit)
        {
            isLit = true;
            flameEffect.SetActive(true);
        }
        else
        {
            isLit = false;
            flameEffect.SetActive(false);
        }
    }

    public void Hit(bool onFire)
    {
        // Light brazier
        if (onFire && !isLit)
        {
            LightBrazier();
            CheckpointManager.SaveCheckpoint();
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
        flameEffect.SetActive(true);
    }
}
