using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider scoreSlider;
    public GameObject background;
    public RectTransform bulletTraget;
    public List<Character> characters;
    public GameObject gameTime;
    public TextMeshProUGUI gameTimetxt;
    public Workingroom workingroom;
    public GameObject arrow;
    private float gameTimerfloat = 2880;
    private int hour;
    private int minute;
    

    private float score;

    void Awake()
    {
        if(instance != null  && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        //gameTimetxt = gameTime.GetComponent<TextMeshPro>();
        gameTimetxt.text = "48H";
    }
    void Update (){
        if (gameTimerfloat >= 0){
            gameTimerfloat -= Time.deltaTime * 5;
        }

        hour = Mathf.FloorToInt(gameTimerfloat/60);
        minute = Mathf.FloorToInt(gameTimerfloat % 60);
        gameTimetxt.text = hour + "H " + minute;

        float totalProductivity = 0;
        foreach(Character chara in characters){
            totalProductivity += workingroom.GetProductivity(chara);
        }
        totalProductivity /= characters.Count;

        if (totalProductivity >= 75){
            if (!arrow.active){
                arrow.SetActive(true);
            }
        } else {
                if (arrow.active){
                arrow.SetActive(false);
            }
        }

    }

    public float GetScore()
    {
        return score;
    }

    public void Score (float bulletValue)
    {
        background.GetComponent<Animation>().Play();
        score += bulletValue * Time.deltaTime;
        scoreSlider.value = score;
    }

}
