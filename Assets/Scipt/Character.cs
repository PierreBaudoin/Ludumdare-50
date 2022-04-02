using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Stat[] stats;
    private EventData[] possibleEvents;
    private string[] traitList;

    // Start is called before the first frame update
    void Start()
    {
        ///Get Data from CharacterData file
    
        ///Get Local Events from EventData file
    }


    // Update is called once per frame
    void Update()
    {
        ActualizeStats();
        //Roll ?
        CheckEvents(possibleEvents);

        //Productivity
    }

    private void CheckEvents(EventData[] events)
    {
        foreach(EventData ev in events)
        {
            foreach (TriggerCondition trig in ev.possibleTriggers)
            {
                if (trig.Check(traitList, stats))
                {
                    foreach (TargetEffectPair pair in ev.targetEffectPairs)
                    {
                        pair.Play();
                    }
                }
                
            }
        }
    }

    private void ActualizeStats()
    {
        foreach(Stat stat in stats)
        {
            float var = stat.actualValue - (stat.depressionRate * Time.deltaTime);
            stat.Reduce(var);
        }
    }
}

public class Roll
{

}
