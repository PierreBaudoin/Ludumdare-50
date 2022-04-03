using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Box", menuName = "Custom/Event/DialogueBox", order = 2)]
public class DialogBoxEvent : ScriptableObject
{
    public string name;
    [Range(0,1)] public float chance = 1f;
    public string description;
    public TriggerCondition[] possibleTriggers;
    public TargetEffectPair[] targetEffectPairs;
    
}
