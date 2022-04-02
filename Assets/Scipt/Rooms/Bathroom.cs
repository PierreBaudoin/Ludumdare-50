using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : Room
{
    [Header("Bathroom parameters")]
    public string statName = "HYGIENE";
    public float boostRate = 5.0f;

    private Stat savedStat;

    public override void StartUseRoom(Character character)
    {
        base.StartUseRoom(character);
        savedStat = GetStat(character.stats, statName);
        savedStat.LockStat(true);
    }

    public override void UseRoomUpdate(Character character)
    {
        savedStat.Increase(boostRate * Time.deltaTime);
    }

    public override void EndUseRoom(Character character)
    {
        savedStat.LockStat(false);
    }
}
