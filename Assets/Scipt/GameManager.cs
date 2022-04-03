using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider scoreSlider;
    public RectTransform bulletTraget;

    private float score;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple GameManager detected : intance destroyed");
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public float GetScore()
    {
        return score;
    }

    public void Score (float bulletValue)
    {
        score += bulletValue * Time.deltaTime;
        scoreSlider.value = score;
    }

}
