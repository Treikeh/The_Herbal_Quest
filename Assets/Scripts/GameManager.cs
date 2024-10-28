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
    public SharedData sharedData;
    public List<Objective> objectives = new List<Objective>();

    private int currentObjective = 0;


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
        if (currenctGameState == GameStates.Running)
            { return; }

        objectives[currentObjective].currentValue++;
        // Check if currentObjective is completed
        if (objectives[currentObjective].currentValue >= objectives[currentObjective].maxValue)
        {
            objectives[currentObjective].OnObjectiveCompleted.Invoke();
            currentObjective++;
            // Check if all objectives are completed
            if (currentObjective >= objectives.Count) 
                { currenctGameState = GameStates.Finished; }
            else
                { UpdateObjectiveString(); }
        }
        else
            { UpdateObjectiveString(); }
    }

    private void UpdateObjectiveString()
    {
        string title = objectives[currentObjective].title;
        if (objectives[currentObjective].displayValues == true)
        {
            string maxValue = objectives[currentObjective].maxValue.ToString();
            string currentValue = objectives[currentObjective].currentValue.ToString();
            sharedData.objectiveText = title + currentValue + " / " + maxValue;
        }
        else
        {
            sharedData.objectiveText = title;
        }
    }
}

[System.Serializable]
public class Objective
{
    public string title;
    public int maxValue;
    public int currentValue;
    public bool displayValues = false;
    public UnityEvent OnObjectiveCompleted;
}