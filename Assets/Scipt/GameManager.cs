using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RectTransform mainCanvas;
    public Slider scoreSlider;
    public GameObject background;
    public RectTransform bulletTraget;
    public List<Character> characters;
    public GameObject gameTime;
    public TextMeshProUGUI gameTimetxt;
    public Workingroom workingroom;
    public GameObject arrow;
    public GameOverScreen gameOverScreen;
    public GameObject characterPrefab;
    public Transform characterParent;
    public List<CharacterData> characterdata;
    public float timeScale = 10.0f;
    private float gameTimerfloat = 2880;
    private bool isStarted = false;
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

        characters = new List<Character>();
    }

    void Start()
    {
        //gameTimetxt = gameTime.GetComponent<TextMeshPro>();
        gameTimetxt.text = "48H";
    }
    void Update (){
        if (gameTimerfloat > 0){
            gameTimerfloat -= Time.deltaTime * timeScale;
        }
        else
        {
            FinishGame();
        }
        if (gameTimerfloat <= 800)
            SoundManager2D.StartEndMusic();

        hour = Mathf.FloorToInt(gameTimerfloat/60);
        minute = Mathf.FloorToInt(gameTimerfloat % 60);
        gameTimetxt.text = hour + "H " + minute;

//----------------------------------------------------------------------
        float totalProductivity = 0;
        foreach(Character chara in characters){
            totalProductivity += workingroom.GetProductivity(chara);
        }
        //Debug.Log ("prod1 : " + totalProductivity);
        totalProductivity /= characters.Count;
        //Debug.Log ("prod : " + totalProductivity);
        if (totalProductivity >= .75f){
            if (!arrow.activeInHierarchy){
                arrow.SetActive(true);
            }
        } else {
                if (arrow.activeInHierarchy){
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
        //background.GetComponent<Animation>().Play();
        score += bulletValue;
        scoreSlider.value = score;
        if(scoreSlider.value == scoreSlider.maxValue)
        {
            //gameTimerfloat = 0f;
        }
    }

    public void StartGame()
    {
        isStarted = true;

        foreach(CharacterData c in characterdata)
        {
            GameObject g = Instantiate(characterPrefab, characterParent);
            g.GetComponent<Character>().characterData = c;
        }
    }

    public bool isGameStarded()
    {
        return isStarted;
    }

    private void FinishGame()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.StartGameOverSequence(score);
        SoundManager2D.StartMenuMusic();
    }
}
