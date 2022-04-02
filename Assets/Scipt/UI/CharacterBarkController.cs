using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules.Translations.Controllers;

public class CharacterBarkController : MonoBehaviour
{
    public TextUGUITranslationController barkText;
    public Canvas canvas;

    public void ShowText(string key)
    {
        canvas.gameObject.SetActive(true);

        this.barkText.key = key;
        this.barkText.ManualUpdateText();
    }

    public void ShowText(string key, float duration)
    {
        this.ShowText(key);
        Timer t = new Timer(duration, HideText);
        t.ResetPlay();
    }

    public void HideText()
    {
        canvas.gameObject.SetActive(false);
    }
}
