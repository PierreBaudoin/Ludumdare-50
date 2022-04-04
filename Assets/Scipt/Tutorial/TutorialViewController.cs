using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules.Translations.Controllers;

public class TutorialViewController : MonoBehaviour
{
    public string textKey;
    public TextUGUITranslationController textZone;
    public bool useTimer = false;
    public float duration = 5f;
    public GameObject oKButton;
    private Timer hideTimer;

    void Awake()
    {
        if(useTimer)
        {
            hideTimer = new Timer(duration, HideView);
            oKButton.SetActive(false);
        }
        else
        {
            oKButton.SetActive(true);
        }

        textZone.key = textKey;
        textZone.ManualUpdateText();
    }

    void OnEnable()
    {
        if(useTimer)
        {
            hideTimer.ResetPlay();
        }
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }
}
