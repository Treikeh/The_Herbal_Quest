using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// This component is responsable for handling game systems like winning, losing and progression
// This is a simple test

public class GameManager : MonoBehaviour
{
    public enum GameStates {
        Running,
        Paused,
        Finished,
    }
    
    [HideInInspector] public static GameStates currenctGameState;
    [SerializeField] private List<Objective> _objectives = new List<Objective>();

    private int _currentObjective = 0;

    public static Action<string> UpdateObjectiveText;


    private void OnEnable()
    {
        // Reset values when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Reset values when a new scene is loaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Rest pause when switching scenes
        Time.timeScale = 1f;
        currenctGameState = GameStates.Running;
    }


    private void Start()
    {
        // Limit frame rate
        Application.targetFrameRate = 90;
        UpdateObjectiveString();
    }

    public void AddObjectiveProgress()
    {
        if (currenctGameState != GameStates.Running)
            { return; }

        _objectives[_currentObjective].CurrentValue++;
        // Check if _currentObjective is completed
        if (_objectives[_currentObjective].CurrentValue >= _objectives[_currentObjective].MaxValue)
        {
            _objectives[_currentObjective].OnObjectiveCompleted.Invoke();
            _currentObjective++;
            // Check if all _objectives are completed
            if (_currentObjective >= _objectives.Count) 
                { currenctGameState = GameStates.Finished; }
            else
                { UpdateObjectiveString(); }
        }
        else
            { UpdateObjectiveString(); }
    }

    private void UpdateObjectiveString()
    {
        string Title = _objectives[_currentObjective].Title;
        if (_objectives[_currentObjective].DisplayValues == true)
        {
            string MaxValue = _objectives[_currentObjective].MaxValue.ToString();
            string CurrentValue = _objectives[_currentObjective].CurrentValue.ToString();
            UpdateObjectiveText?.Invoke(Title + CurrentValue + " / " + MaxValue);
        }
        else
        {
            UpdateObjectiveText?.Invoke(Title);
        }
    }
}

[System.Serializable]
public class Objective
{
    public string Title;
    public int MaxValue;
    public int CurrentValue;
    public bool DisplayValues = false;
    public UnityEvent OnObjectiveCompleted;
}