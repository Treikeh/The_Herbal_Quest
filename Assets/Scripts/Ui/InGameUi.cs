using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUi : MonoBehaviour
{
    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;


    public void OnPause()
    {
        // Pause Game
        if (GameManager.currenctGameState == GameManager.GameStates.Running)
        {
            OnGamePaused.Invoke();
            GameManager.currenctGameState = GameManager.GameStates.Paused;
            //isGamePaused.value = true;
            Time.timeScale = 0f;
        }
        // Resume Game
        else if (GameManager.currenctGameState == GameManager.GameStates.Paused)
        {
            OnGameResumed.Invoke();
            GameManager.currenctGameState = GameManager.GameStates.Running;
            //isGamePaused.value = false;
            Time.timeScale = 1f;
        }
    }
}
