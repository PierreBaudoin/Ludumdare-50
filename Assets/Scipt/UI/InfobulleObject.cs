using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfobulleObject : MonoBehaviour
{
    public float timerBeforeInfobulle = 3.0f;
    [HideInInspector]
    public bool mouseOnObject = false;
    public GameObject toHide;

    private void Start()
    {
        Hide();
    }

    private void OnMouseEnter()
    {
        InfobulleSpawner.instance.StartFillerTimer(timerBeforeInfobulle, Display, Hide);
        mouseOnObject = true;
    }

    private void OnMouseExit()
    {
        InfobulleSpawner.instance.StopFillerTimer();
        InfobulleSpawner.instance.RemoveInfobulleIfAny();
        mouseOnObject = true;
    }


    public void Display()
    {
        Debug.Log("Display");
        toHide.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Hide");
        toHide.SetActive(false);   
    }
}
