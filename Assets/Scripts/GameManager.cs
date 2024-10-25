using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This component is responsable for handling game systems like winning, losing and progression
// This is a simple test

public class GameManager : MonoBehaviour
{
    public StringAsset objectiveString;
    public List<Objective> objectives = new List<Objective>();

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
        if (isGameOver)
        {
            return;
        }

        objectives[currentObjective].currentValue++;
        // Check if currentObjective is completed
        if (objectives[currentObjective].currentValue >= objectives[currentObjective].maxValue)
        {
            objectives[currentObjective].OnObjectiveCompleted.Invoke();
            currentObjective++;
            // Check if all objectives are completed
            if (currentObjective >= objectives.Count)
            {
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
        string title = objectives[currentObjective].title;
        if (objectives[currentObjective].displayValues == true)
        {
            string maxValue = objectives[currentObjective].maxValue.ToString();
            string currentValue = objectives[currentObjective].currentValue.ToString();
            objectiveString.value = title + currentValue + " / " + maxValue;
        }
        else
        {
            objectiveString.value = title;
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