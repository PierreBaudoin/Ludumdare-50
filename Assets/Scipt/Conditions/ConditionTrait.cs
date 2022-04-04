using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConditionTrait : Condition
{
    public string traitName;
    public override bool IsValid(Character target)
    {
        foreach(string s in target.characterData.traits)
        {
            if(s == traitName)
            {
                return true;
            }
        }

        return false;
    }
}
