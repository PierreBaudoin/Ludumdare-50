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
        print ("TIMER : " + gameTimerfloat);
        if (gameTimerfloat >= 0){
            gameTimerfloat -= Time.deltaTime * 5;
        }

        hour = Mathf.FloorToInt(gameTimerfloat/60);
        minute = Mathf.FloorToInt(gameTimerfloat % 60);
        gameTimetxt.text = hour + "H " + minute;
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
