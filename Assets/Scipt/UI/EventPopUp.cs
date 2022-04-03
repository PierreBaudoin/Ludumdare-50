using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules.Translations.Controllers;

public class EventPopUp : MonoBehaviour
{
    [SerializeField] private TextUGUITranslationController description;
    public void Init(string textKey)
    {
        description.key = textKey;
        description.ManualUpdateText();
    }

    public void OKClick()
    {
        this.gameObject.SetActive(false);
    }
}
