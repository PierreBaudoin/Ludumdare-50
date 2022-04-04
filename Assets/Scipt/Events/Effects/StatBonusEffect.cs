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

    public override void Update(Character[] targets)
    {
    
    }
}
