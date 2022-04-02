using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Custom/Character", order = 2)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public string description;
    public GameObject model;
    public Stat[] stats =
    {
        new Stat("HUNGER", 100), new Stat("FATIGUE", 100), new Stat("HYGIENE", 100), new Stat("MORAL", 100)
    };

    public string[] traits;
}


[Serializable]
public class Stat
{
    public string statName;
    public float maxValue;
    public float depressionRate;
    [HideInInspector]
    public float actualValue;

    private static float defaultDepressionRate = 1.0f;
    private bool lockedStat = false;


    public Stat(string statName, float maxValue)
    {
        this.statName = statName;
        this.maxValue = maxValue;
        actualValue = maxValue;
        depressionRate = defaultDepressionRate;
        lockedStat = false;
    }

    public void LockStat(bool locked)
    {
        lockedStat = locked;
    }

    public bool IsStatLocked()
    {
        return lockedStat;
    }

    public void Reduce(float amount)
    {
        actualValue -= amount;
        if (actualValue <= 0)
        {
            actualValue = 0;
        }
    }

    public void Increase(float amount)
    {
        actualValue += amount;
        if (actualValue > maxValue)
        {
            actualValue = maxValue;
        }
    }
}
