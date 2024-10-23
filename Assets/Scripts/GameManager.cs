using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    public BoolAsset isGameOver;


    private void Start()
    {
        // Limit frame rate
        Application.targetFrameRate = 90;
    }

    public void OnGameOver()
    {
        isGameOver.value = true;
    }
}