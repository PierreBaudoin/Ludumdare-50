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

    private int actualNumberOfCharacters = 0;
    private List<Character> charactersInRoom;
    private List<Transform> availableTransforms;
    private List<Transform> occupiedTransforms;

    protected virtual void Start()
    {
        foreach (Transform tr in slotsTransform)
        {
            availableTransforms.Add(tr);
        }
    }

    public bool IsRoomFull()
    {
        if (actualNumberOfCharacters == numberOfSlots)
        {
            return true;
        }
        else
        {
            return false;
        }
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
        actualNumberOfCharacters++;
        charactersInRoom.Add(character);
        Transform result = availableTransforms[0];
        availableTransforms.Remove(result);
        occupiedTransforms.Add(result);
        return result;
    }

    public void RemoveCharacter(Character character, Transform slotTransform)
    {
        actualNumberOfCharacters--;
        charactersInRoom.Remove(character);
        occupiedTransforms.Remove(slotTransform);
        availableTransforms.Add(slotTransform);
    }
}
