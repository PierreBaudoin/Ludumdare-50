using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public float minTimerBetweenEvent = 15.0f;
    public float maxTimerBetweenEvent = 20.0f;

    [SerializeField] private GameObject EventPopUpGameObject;
    [SerializeField] private List<DialogBoxEvent> dialogBoxEvents;
    private List<DialogBoxEvent> usedEffects;
    private bool launchEvent = false;

    private string sourceCharacterName;
    private string affectedCharacterName;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple EventManager detected : instance destroyed");
            Destroy(this.gameObject);
        }
        instance = this;

        usedEffects = new List<DialogBoxEvent>();
        (new Timer(Random.Range(minTimerBetweenEvent, maxTimerBetweenEvent), SwapVariableLaunchEvent)).Play();

        ShuffleEvents();
    }

    void Update()
    {
        if (launchEvent)
        {
            CheckEvent();

        }
    }

    private void ShuffleEvents()
    {
        for (int i = 0; i < dialogBoxEvents.Count; i++) {
             DialogBoxEvent temp = dialogBoxEvents[i];
             int randomIndex = Random.Range(i, dialogBoxEvents.Count);
             dialogBoxEvents[i] = dialogBoxEvents[randomIndex];
             dialogBoxEvents[randomIndex] = temp;
         }
    }

    public void SwapVariableLaunchEvent()
    {
        launchEvent = !launchEvent;
    }

    private void CheckEvent()
    {
        print("Event");
        foreach (DialogBoxEvent d in dialogBoxEvents)
        {
            foreach (Character c in GameManager.instance.characters)
            {
                if (d.IsActive(c))
                {
                    print("Active" + d.name);
                    foreach (TargetEffectPair t in d.targetEffectPairs)
                    {
                        if (usedEffects.Contains(d) == false)
                        {
                            PlayDialogBoxEffect(d, c, t);
                            sourceCharacterName = c.characterData.characterName;
                            launchEvent = false;
                            (new Timer(Random.Range(minTimerBetweenEvent, maxTimerBetweenEvent), SwapVariableLaunchEvent)).Play();
                            return;
                        }
                    }
                }
            }
        }
    }

    private void PlayDialogBoxEffect(DialogBoxEvent d, Character target, TargetEffectPair t)
    {
        Debug.Log(d + " -- " + target.characterData.name);
        affectedCharacterName = target.characterData.characterName;
        t.Play(target);
        DisplayEvent(d);
        usedEffects.Add(d);
    }

    public void DisplayEvent(DialogBoxEvent d)
    {
        EventPopUpGameObject.GetComponent<EventPopUp>().gameObject.SetActive(true);
        string result = d.textToDisplay;
        result = result.Replace("c1", sourceCharacterName);
        result = result.Replace("c2", affectedCharacterName);
        EventPopUpGameObject.GetComponent<EventPopUp>().Init(result);
    }
}
