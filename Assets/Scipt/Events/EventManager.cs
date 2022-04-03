using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EventManager : MonoBehaviour
{
    public static EventManager instance;



    private DialogBoxEvent[] dialogBoxEvents;


    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple EventManager detected : instance destroyed");
            Destroy(this.gameObject);
        }
        instance = this;

        GatherDialogueBoxEvents();
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
                        t.Play(d.useOncePerGame);
                    }
                }
            }
        }
    }

    private void GatherDialogueBoxEvents()
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(DialogBoxEvent).Name);
        
        dialogBoxEvents = new DialogBoxEvent[guids.Length];
        for(int i =0;i<guids.Length;i++) 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            dialogBoxEvents[i] = AssetDatabase.LoadAssetAtPath<DialogBoxEvent>(path);
        }
    }
}
