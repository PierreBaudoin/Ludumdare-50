using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Custom/Event", order = 1)]
public class EventData : ScriptableObject
{
    public float chance = 1.0f;
    public TriggerCondition[] possibleTriggers;
    public TargetEffectPair[] targetEffectPairs;

    public enum EventType
    {
        Global, Local
    }

    public EventType type = EventType.Local;
}

[Serializable]
public class TriggerCondition
{
    public enum TriggerType
    {
        AND, OR
    }

    public TriggerType triggerType = TriggerType.AND;
    public Condition[] conditions;

    public bool Check(string[] traitList, Stat[] statList)
    {
        switch (triggerType)
        {
            case TriggerType.AND:
                foreach (Condition c in conditions)
                {
                    if (c.Check(traitList, statList) == false)
                    {
                        return false;
                    }
                }
                return true;

            case TriggerType.OR:
                foreach (Condition c in conditions)
                {
                    if (c.Check(traitList, statList))
                    {
                        return true;
                    }
                }
                return false;
        }
        return false;
    }
}

[Serializable]
public class Condition
{
    public enum ConditionType
    {
        CheckStat, CheckTrait
    }
    public ConditionType type;
    public string statName;
    public float statValue;
    public string traitName;

    public bool Check(string[] traitList, Stat[] statList)
    {
        switch (type)
        {
            case ConditionType.CheckStat:
                foreach(Stat stat in statList)
                {
                    if (stat.statName == statName && stat.actualValue <= statValue)
                    {
                        return true;
                    }
                }
                return false;


            case ConditionType.CheckTrait:
                foreach(string str in traitList)
                {
                    if (str.Equals(traitName))
                    {
                        return true;
                    }
                }
                return false;
        }
        return false;
    }
}

[Serializable]
public class TargetEffectPair
{
    public Effect[] effects;
    public enum TargetingRule
    {
        ThisCharacter, AdjacentCharacter, RandomCharacter
    }
    public TargetingRule targetingRule;
    public void Play()
    {
        ////TO DO
    }
}

[Serializable]
public class Effect
{
    public enum EffectType
    {
        AlterStat, MoveCharacter, ApplyCondition
    }

    public float alterStatValue;
    ///Each effect behavior must implement its behaviour from here.
    ///Potentially ->  Create a EffectData scriptable object if easier to manager
    ///Legacy to vary effectType ?
}