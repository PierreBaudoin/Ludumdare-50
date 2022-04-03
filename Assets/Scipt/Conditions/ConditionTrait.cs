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
        Debug.LogError("Not yet implemented");
        return false;
    }
}
