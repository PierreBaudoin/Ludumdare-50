using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterIdViewController : MonoBehaviour
{
    public TextMeshProUGUI nom, description;
    public TextMeshProUGUI hungerText, fatigueText, hygieneText, moralText;
    public Slider hunger, fatigue, hygiene, moral;
    public Image emote;
    
    private Character character;
    
    public void OpenView(Character character)
    {
        this.character = character;
        
        /*nom.text = character.name;
        description.text = character.description;*/

    }

    void Update()
    {
        UpdateValues();
    }

    private void UpdateValues(){
        
        hunger.value = 17;
        fatigue.value = 25;
        hygiene.value = 75;
        moral.value = 68;
        

        hungerText.text = "" + hunger.value;
        fatigueText.text = "" + fatigue.value;
        hygieneText.text = "" + hygiene.value;
        moralText.text = "" + moral.value;
    }
}
