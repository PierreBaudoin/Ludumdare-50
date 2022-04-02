using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterIdViewController : MonoBehaviour
{
    public TextMeshProUGUI nom, description;
    public TextMeshProUGUI faimText, fatigueText, hygieneText, moralText;
    public Slider faim, fatigue, hygiene, moral;
    public Image emote;
    /*
    private Character character;
    
    public void OpenView(Character character)
    {
        this.character = character;
        
        nom.text = chracter.name;
        description.text = character.description;

    }*/

    void Update()
    {
        UpdateValues();
    }

    private void UpdateValues(){
        /*
        faim.value = character.faim;
        fatigue.value = character.fatigue;
        hygiene.value = character.hygiene;
        moral.value = character.moral;
        */

        faimText.text = "" + faim.value;
        fatigueText.text = "" + fatigue.value;
        hygieneText.text = "" + hygiene.value;
        moralText.text = "" + moral.value;
    }
}
