using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is responsable for handling game systems like winning, losing and progression

public class GameManager : MonoBehaviour
{
    public StringAsset objectiveString;
    public List<Objective> objectives = new List<Objective>();
    public UnityEvent AllObjectivesCompleted;

    public static bool isGameOver;
    public static bool isGamePaused;

    private int currentObjective = 0;


    private void Start()
    {
        // Limit frame rate
        Application.targetFrameRate = 90;
        UpdateObjectiveString();
    }

    public void AddObjectiveProgress()
    {
        objectives[currentObjective].currentValue++;
        // Check if currentObjective is completed
        if (objectives[currentObjective].currentValue >= objectives[currentObjective].maxValue)
        {
            objectives[currentObjective].OnObjectiveCompleted.Invoke();
            currentObjective++;
            // Check if all objectives are completed
            if (currentObjective >= objectives.Count)
            {
                AllObjectivesCompleted.Invoke();
                isGameOver = true;
            }
            else
            {
                UpdateObjectiveString();
            }
        }
        else
        {
            UpdateObjectiveString();
        }
    }

    private void UpdateObjectiveString()
    {
        string description = objectives[currentObjective].description;
        if (objectives[currentObjective].displayValues == true)
        {
            string maxValue = objectives[currentObjective].maxValue.ToString();
            string currentValue = objectives[currentObjective].currentValue.ToString();
            objectiveString.value = description + currentValue + " / " + maxValue;
        }
        else
        {
            objectiveString.value = description;
        }
    }
}

[System.Serializable]
public class Objective
{
    public string title;
    public string description;
    public int maxValue;
    public int currentValue;
    public bool displayValues = false;
    public UnityEvent OnObjectiveCompleted;
}