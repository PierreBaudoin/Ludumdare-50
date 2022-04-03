using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : Room
{
    [Header("Bedroom parameters")]
    public string statName = "FATIGUE";
    public float boostRate = 5.0f;

    private Dictionary<Character, Stat> dictionary;

    void Awake()
    {
        dictionary = new Dictionary<Character, Stat>();
    }

    public override void StartUseRoom(Character character)
    {
        Stat var = GetStat(character.stats, statName);
        var.LockStat(true);
        dictionary.Add(character, var);
    }

    public override void UseRoomUpdate(Character character)
    {
        Stat var;
        dictionary.TryGetValue(character, out var);
        var.Increase(boostRate * Time.deltaTime);
    }

    public override void EndUseRoom(Character character)
    {
        Stat var;
        dictionary.TryGetValue(character, out var);
        var.LockStat(false);
        dictionary.Remove(character);
    }
}
