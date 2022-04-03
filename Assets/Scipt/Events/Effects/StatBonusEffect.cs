using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatBonusEffect : Effect
{
    public string statName;
    public float statBonus;
    public override void Play(Character[] targets)
    {
        Debug.Log("Effect");
        foreach(Character c in targets)
        {
            foreach(Stat s in c.stats)
            {
                if(s.statName == this.statName)
                {
                    s.Increase(statBonus);
                }
            }
        }
    }
}
