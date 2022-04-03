using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Box", menuName = "Custom/Event/DialogueBox", order = 2)]
public class DialogBoxEvent : ScriptableObject
{
    public string name;
    public bool useInGame = true;
    public bool useOncePerGame;
    [Range(0,1)] public float chance = 1f;
    public string textToDisplay;
    public TriggerCondition[] possibleTriggers;
    public TargetEffectPair[] targetEffectPairs;
    
    public bool IsActive(Character target)
    {
        foreach(TriggerCondition t in possibleTriggers)
        {
            if(t.triggerType == TriggerCondition.TriggerType.AND)
            {
                return CheckAND(t.conditionsStats, target) && CheckAND(t.conditionsTraits, target);
            }
            else
            {
                return CheckOR(t.conditionsStats, target) || CheckOR(t.conditionsTraits, target);
            }
        }
        return false;
    }

    private bool CheckOR(Condition[] conditions, Character target)
    {
        foreach(Condition c in conditions)
        {
            if(c.IsValid(target) == true)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckAND(Condition[] conditions, Character target)
    {
        foreach(Condition c in conditions)
        {
            if(c.IsValid(target) == false)
            {
                break;
            }
        }
        return true;
    }
}
