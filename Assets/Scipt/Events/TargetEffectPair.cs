using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TargetEffectPair
{
    public TargetingRule targetingRule;
    public StatBonusEffect[] statBonusEffects;

    public bool used = false;

    public void Play(Character target)
    {
        used = true;
        List<Effect> effects = FillEffectList(statBonusEffects);

        foreach(Effect e in effects)
        {
            switch(targetingRule)
            {
                case TargetingRule.ThisCharacter:
                e.Play(new Character[] {target});
                break;

                case TargetingRule.AdjacentCharacter:
                foreach(Room r in MonoBehaviour.FindObjectsOfType<Room>()) // Pour chaque room
                        {
                            foreach(Transform t in r.validPositions.Keys) // Pour chaque Character
                            {
                                if(r.validPositions[t] == target)   // Si la room contient la target
                                {
                                    List<Character> charac = new List<Character>();
                                    foreach(Transform tr in r.validPositions.Keys)  // Pour chaque caracter de la room
                                    {
                                        if(r.validPositions[tr] != null && r.validPositions[tr] != target)  // s'il existe et que c'est pas la cible
                                        {
                                            charac.Add(r.validPositions[tr]);   // on  l'ajoute à la liste
                                        }
                                    }
                                    e.Play(charac.ToArray());
                                    return;
                                }
                            }
                        }
                break;

                case TargetingRule.RandomCharacter:
                e.Play(new Character[] {GameManager.instance.characters[UnityEngine.Random.Range(0, GameManager.instance.characters.Count)]});
                break;
            }
        }
    }

    private List<Effect> FillEffectList(Effect[] effects)
    {
        List<Effect> result = new List<Effect>();

        foreach(Effect e in effects)
        {
            result.Add(e);
        }

        return result;
    }
}

public enum TargetingRule
{
    ThisCharacter, AdjacentCharacter, RandomCharacter
}