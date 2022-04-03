using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playingroom : Room
{
    [Header("Playing room parameters")]
    public string statName = "MORAL";
    public float boostRate = 5.0f;
    public int numberOfParticipantNeeded = 2;
    private bool isPlaying = false;

    private Dictionary<Character, Stat> dictionary;

    protected override void Start()
    {
        base.Start();
        dictionary = new Dictionary<Character, Stat>();
    }

    private void Update()
    {
        ActualizeIsPlaying();    
    }

    public override void StartUseRoom(Character character)
    {
        Stat var = GetStat(character.stats, statName);
        dictionary.Add(character, var);
    }

    public override void UseRoomUpdate(Character character)
    {
        if (isPlaying)
        {
            Stat var;
            dictionary.TryGetValue(character, out var);
            var.Increase(boostRate * Time.deltaTime);
        }
    }

    private void ActualizeIsPlaying()
    {
        if (GetNumberOfFilledSlots() >= numberOfParticipantNeeded)
        {
            isPlaying = true;
            foreach(KeyValuePair<Character, Stat> kv in dictionary)
            {
                if (kv.Value != null)
                    kv.Value.LockStat(true);
            }
        }
        else if (GetNumberOfFilledSlots() < numberOfParticipantNeeded)
        {
            isPlaying = false;
            foreach (KeyValuePair<Character, Stat> kv in dictionary)
            {
                if (kv.Value != null)
                    kv.Value.LockStat(false);
            }
        }
    }

    public override void EndUseRoom(Character character)
    {
        Stat var;
        dictionary.TryGetValue(character, out var);
        dictionary.Remove(character);
        var.LockStat(false);
    }
}
