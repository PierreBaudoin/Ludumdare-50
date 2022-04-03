using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EventManager : MonoBehaviour
{
    public static EventManager instance;



    private List<DialogBoxEvent> dialogBoxEvents;


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
                        t.Play(d.useOncePerGame, c);
                    }
                }
            }
        }
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
}
