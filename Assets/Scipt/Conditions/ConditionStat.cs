using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConditionStat : Condition
{
    public string statName;
    public float statValue;
    public ConditionComparator comparator;

    public override bool IsValid(Character target)
    {
        foreach(Stat s in target.stats)
        {
            if(s.statName == statName)
            {
                switch(comparator)
                {
                    case ConditionComparator.inf:
                        return s.actualValue < statValue;

                    case ConditionComparator.sup:
                        return s.actualValue > statValue;

                    case ConditionComparator.equ:
                        return s.actualValue == statValue;

                    case ConditionComparator.infEqu:
                        return s.actualValue <= statValue;

                    case ConditionComparator.supEqu:
                        return s.actualValue >= statValue;

                    case ConditionComparator.dif:
                        return s.actualValue != statValue;

                    default : return false;
                }
            }
        }
        Debug.LogWarning("No stat named : " + statName);
        return false;        
    }
}

public enum ConditionComparator
    {
        inf,
        sup,
        equ,
        infEqu,
        supEqu,
        dif
    }