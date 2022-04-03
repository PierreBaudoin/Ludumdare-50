using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfobulleCharacter : InfobulleObject
{
    private Animator anim;
    private CharacterData data;
    private Stat[] stats;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI traitsText;

    public Image postItImage;
    public Image profilePicImage;
    public Slider[] slidersStats;

    private void Start()
    {
        anim = GetComponent<Animator>();
        data = GetComponent<Character>().characterData;
        stats = data.stats;
        nameText.text = data.characterName;
        descriptionText.text = data.description;
        traitsText.text = GetTraits(data.traits);
        postItImage.sprite = data.postIt;
        profilePicImage.sprite = data.profilePic;
        for (int i = 0; i < slidersStats.Length; ++i)
        {
            slidersStats[i].maxValue = stats[i].maxValue;
        }
    }

    private void Update()
    {
        for (int i = 0; i < slidersStats.Length; ++i)
        {
            slidersStats[i].value = stats[i].actualValue;
        }
    }

    private string GetTraits(string[] traits)
    {
        string result = "";
        foreach(string str in traits)
        {
            result += str;
        }
        return result;
    }

    protected override void Display()
    {
        anim.SetBool("open_infobox", true);
    }

    protected override void Hide()
    {
        anim.SetBool("open_infobox", false);
    }
}
