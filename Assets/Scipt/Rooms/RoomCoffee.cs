using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCoffee : Room
{
    [Header("Bathroom parameters")]
    public string statName = "FATIGUE";
    public float boostRate = 30.0f;
    public float maxFatigue = 50.0f;

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
        if (var.actualValue <= maxFatigue)
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
