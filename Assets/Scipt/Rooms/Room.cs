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
    public GameObject tutoView;

    public string animatorParameter = "show_room";
    public Dictionary<Transform, Character> validPositions;

    private Animator roomAnimator;
    private bool hasAnimator = false;

    protected virtual void Start()
    {
        validPositions = new Dictionary<Transform, Character>();
        foreach (Transform tr in slotsTransform)
        {
            validPositions.Add(tr, null);
        }
        roomAnimator = GetComponent<Animator>();
        if (roomAnimator != null) hasAnimator = true;
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

    public void SetVisibility(bool visibility)
    {
        SetActiveAll(toActivateWhenVisible, visibility);
        SetActiveAll(toDeactivateWhenVisible, !visibility);
        SetActiveAll(toActivateWhenInvisible, !visibility);
        SetActiveAll(toDeactivateWhenInvisible, visibility);
        if (GetNumberOfFilledSlots() != 0)
        {
            foreach (Character character in validPositions.Values)
            {
                character.Appear();
            }
        }
        if (hasAnimator)
        {
            roomAnimator.SetBool(animatorParameter, visibility);
        }
        
        TutorialManager.instance.ShowTutoView(tutoView.GetComponent<TutorialViewController>());
    }

    public virtual Transform AddCharacter(Character character)
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
