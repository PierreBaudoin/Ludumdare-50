using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfobulleSpawner : MonoBehaviour
{
    private RectTransform rtr;
    private Timer t;

    private void Start()
    {
        rtr = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rtr.localPosition = Input.mousePosition;
    }

    public void StartTimer(float duration)
    {

    }

    private void ActualizeTimer()
    {

    }

    public void StopTimer()
    {

    }


}
