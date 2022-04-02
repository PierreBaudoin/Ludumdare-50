using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveMenuPage : MenuPage
{
    public void ClickYes()
    {
        Application.Quit();
    }

    public void ClickNo()
    {
        MenuManager.instance.Return();
    }
}
