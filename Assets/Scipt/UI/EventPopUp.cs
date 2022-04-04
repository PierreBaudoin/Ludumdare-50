using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules.Translations.Controllers;
using TMPro;

public class EventPopUp : MonoBehaviour
{
    [SerializeField] private TextUGUITranslationController description;
    public TextMeshProUGUI debugText;
    public void Init(string textKey)
    {
        //description.key = textKey;
        //description.ManualUpdateText();
        debugText.text = textKey;
    }

    public void OKClick()
    {
        this.gameObject.SetActive(false);
    }
}
