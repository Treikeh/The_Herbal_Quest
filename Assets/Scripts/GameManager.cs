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
    
    [HideInInspector] public static GameStates CurrenctGameState;
    [HideInInspector] public static Vector3 CheckpointPosition;

    [SerializeField] private List<Objective> objectives = new List<Objective>();
    private int currentObjective = 0;

    public static Action CheckpointReload;
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
        CurrenctGameState = GameStates.Running;
    }


    private void Start()
    {
        // Limit frame rate
        Application.targetFrameRate = 90;
        UpdateObjectiveString();
    }

    public static void ReloadCheckpoint()
    {
        CheckpointReload?.Invoke();
        CurrenctGameState = GameStates.Running;
    }

    public void AddObjectiveProgress()
    {
        if (CurrenctGameState != GameStates.Running)
            { return; }

        objectives[currentObjective].CurrentValue++;
        // Check if currentObjective is completed
        if (objectives[currentObjective].CurrentValue >= objectives[currentObjective].MaxValue)
        {
            objectives[currentObjective].OnObjectiveCompleted.Invoke();
            currentObjective++;
            // Check if all objectives are completed
            if (currentObjective >= objectives.Count) 
                { CurrenctGameState = GameStates.Finished; }
            else
                { UpdateObjectiveString(); }
        }
        else
            { UpdateObjectiveString(); }
    }

    private void UpdateObjectiveString()
    {
        string Title = objectives[currentObjective].Title;
        if (objectives[currentObjective].DisplayValues == true)
        {
            string MaxValue = objectives[currentObjective].MaxValue.ToString();
            string CurrentValue = objectives[currentObjective].CurrentValue.ToString();
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