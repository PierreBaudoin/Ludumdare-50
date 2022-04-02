using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    void Update()
    {
        if(MenuManager.instance.GetCurrentPage() != this)
        {
            gameObject.SetActive(false);
        }
    }

    public void Return()
    {
        MenuManager.instance.Return();
    }
}
