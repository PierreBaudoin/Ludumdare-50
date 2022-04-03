using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Condition
{
    string name;

    public abstract bool IsValid(Character target);
}
