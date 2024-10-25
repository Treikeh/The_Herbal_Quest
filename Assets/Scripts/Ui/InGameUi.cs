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
        if (GameManager.isGameOver == true)
        {
            return;
        }

        if (GameManager.isGamePaused == true)
        {
            // Resume Game
            OnGameResumed.Invoke();
            GameManager.isGamePaused = false;
            //isGamePaused.value = false;
            Time.timeScale = 1f;
        }
        else
        {
            // Pause Game
            OnGamePaused.Invoke();
            GameManager.isGamePaused = true;
            //isGamePaused.value = true;
            Time.timeScale = 0f;
        }
    }
}
