using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int numberOfSlots;
    public Transform[] slotsTransform;
    public GameObject[] toActivateWhenVisible;
    public GameObject[] toDeactivateWhenVisible;
    public GameObject[] toActivateWhenInvisible;
    public GameObject[] toDeactivateWhenInvisible;

    public Dictionary<Transform, Character> validPositions;

    protected virtual void Start()
    {
        validPositions = new Dictionary<Transform, Character>();
        foreach (Transform tr in slotsTransform)
        {
            validPositions.Add(tr, null);
        }
    }

    public bool IsRoomFull()
    {
        return GetNumberOfFilledSlots() == numberOfSlots;
    }

    public int GetNumberOfFilledSlots()
    {
        int result = 0;
        foreach(KeyValuePair<Transform, Character> kv in validPositions)
        {
            if(kv.Value != null)
                result ++;
        }
        return result;
    }

    private void SetActiveAll(GameObject[] array, bool value)
    {
        foreach(GameObject go in array)
        {
            go.SetActive(value);
        }
    }

    public void SetVisible()
    {
        SetActiveAll(toActivateWhenVisible, true);
        SetActiveAll(toDeactivateWhenVisible, false);
    }

    public void SetInvisible()
    {
        SetActiveAll(toActivateWhenInvisible, true);
        SetActiveAll(toDeactivateWhenInvisible, false);
    }

    public Transform AddCharacter(Character character)
    {
        Transform result;
        if (IsRoomFull())
            return null;
        foreach(Transform tr in validPositions.Keys)
        {
            if (validPositions[tr] == null)
            {
                result = tr;
                validPositions[tr] = character;
                return result;
            }
        }
        return null;
    }

    public void RemoveCharacter(Character character)
    {
        Transform bite = null;
        foreach(Transform tr in validPositions.Keys)
        {
            if (validPositions[tr] == character)
            {
                bite = tr;
                break;
            }
        }
        Debug.Log(bite);
        validPositions[bite] = null;
    }

    public virtual void UseRoomUpdate(Character character)
    {

    }

    public virtual void StartUseRoom(Character character)
    {

    }

    public virtual void EndUseRoom(Character character)
    {

    }

    public static Stat GetStat(Stat[] stats, string statName)
    {
        foreach(Stat stat in stats)
        {
            if (stat.statName == statName)
            {
                return stat;
            }
        }
        Debug.LogError("Stat not found!");
        return null;
    }
}
