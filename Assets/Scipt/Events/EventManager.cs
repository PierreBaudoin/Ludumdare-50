using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [SerializeField] private GameObject EventPopUpPrefab;
    private EventPopUp eventPopUp;
    private List<DialogBoxEvent> dialogBoxEvents;
    private List<DialogBoxEvent> usedEffects;


    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple EventManager detected : instance destroyed");
            Destroy(this.gameObject);
        }
        instance = this;

        GatherDialogueBoxEvents();
        usedEffects = new List<DialogBoxEvent>();
    }

    void Update()
    {
        foreach(Character c in GameManager.instance.characters)
        {
            foreach(DialogBoxEvent d in dialogBoxEvents)
            {
                if(d.IsActive(c))
                {
                    foreach(TargetEffectPair t in d.targetEffectPairs)
                    {
                        if(usedEffects.Contains(d))
                        {
                            if(d.useOncePerGame == false)
                            {
                                PlayDialogBoxEffect(d,c,t);
                            }
                        }
                        else
                        {
                            PlayDialogBoxEffect(d, c, t);
                        }
                    }
                }
            }
        }
    }

    private void PlayDialogBoxEffect(DialogBoxEvent d, Character target, TargetEffectPair t)
    {
        t.Play(target);
        DisplayEvent(d);
        usedEffects.Add(d);
    }

    private void GatherDialogueBoxEvents()
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(DialogBoxEvent).Name);
        
        dialogBoxEvents = new List<DialogBoxEvent>();
        for(int i =0;i<guids.Length;i++) 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            DialogBoxEvent d = AssetDatabase.LoadAssetAtPath<DialogBoxEvent>(path);
            if(d.useInGame) { dialogBoxEvents.Add(d); }
        }
    }

    public void DisplayEvent(DialogBoxEvent d)
    {
        if(eventPopUp == null)
        {
            GameObject g = Instantiate(EventPopUpPrefab, GameManager.instance.scoreSlider.GetComponent<RectTransform>().parent);
            eventPopUp = g.GetComponent<EventPopUp>();
        }
        else
        {
            eventPopUp.gameObject.SetActive(true);
        }

        eventPopUp.Init(d.textToDisplay);
    }
}
