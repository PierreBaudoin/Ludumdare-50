using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    public int numberOfSlots;
    public RoomManager roomManager;
    public TextMeshProUGUI slotText;
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
    private bool visibility = false;

    protected virtual void Start()
    {
        validPositions = new Dictionary<Transform, Character>();
        foreach (Transform tr in slotsTransform)
        {
            validPositions.Add(tr, null);
        }
        roomAnimator = GetComponent<Animator>();
        if (roomAnimator != null) hasAnimator = true;

        slotText.text = this.GetNumberOfFilledSlots() + "/" + this.numberOfSlots;
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
        this.visibility =  visibility;
        SetActiveAll(toActivateWhenVisible, visibility);
        SetActiveAll(toDeactivateWhenVisible, !visibility);
        SetActiveAll(toActivateWhenInvisible, !visibility);
        SetActiveAll(toDeactivateWhenInvisible, visibility);
        if (GetNumberOfFilledSlots() != 0)
        {
            foreach (Character character in validPositions.Values)
            {
                if (character != null)
                    character.Appear();
            }
        }
        if (hasAnimator)
        {
            roomAnimator.SetBool(animatorParameter, visibility);
        }
        foreach(Character c in validPositions.Values)
        {
            if (visibility) c?.Appear();
            if (!visibility) c?.Disappear();
        }
    }

    public virtual Transform AddCharacter(Character character)
    {
        if (visibility) character?.Appear();
        if (!visibility) character?.Disappear();
        Transform result;
        if (IsRoomFull())
            return null;
        foreach(Transform tr in validPositions.Keys)
        {
            if (validPositions[tr] == null)
            {
                result = tr;
                validPositions[tr] = character;
                slotText.text = this.GetNumberOfFilledSlots() + "/" + this.numberOfSlots;
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
        slotText.text = this.GetNumberOfFilledSlots() + "/" + this.numberOfSlots;
    }

    public void OnClick (){
        roomManager.LightOn(this);
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
