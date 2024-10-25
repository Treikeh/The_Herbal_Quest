using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public float cameraSensitivity;
    public string interactPrompt;
    public bool displayAttackIcon;
    [HideInInspector] public float attackDamage;
    [SerializeField] private float defaultDamage;
    public float DefaultDamage
    {
        get{ return defaultDamage; }
    }
}