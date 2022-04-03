using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Custom/Event/Base", order = 1)]
public class EventData : ScriptableObject
{
    [Range(0,1)] public float chance = 1.0f;
    public TriggerCondition[] possibleTriggers;
    public TargetEffectPair[] targetEffectPairs;

    public enum EventType
    {
        Global, Local
    }

    public EventType type = EventType.Local;
}
