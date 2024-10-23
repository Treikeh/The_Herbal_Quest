using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    public BoolAsset isGamePaused;
    public BoolAsset isGameOver;
    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;


    public void OnPause()
    {
        if (isGameOver.value == true)
        {
            return;
        }

        if (isGamePaused.value == true)
        {
            // Resume Game
            OnGameResumed.Invoke();
            isGamePaused.value = false;
            Time.timeScale = 1f;
        }
        else
        {
            // Pause Game
            OnGamePaused.Invoke();
            isGamePaused.value = true;
            Time.timeScale = 0f;
        }
    }
}
