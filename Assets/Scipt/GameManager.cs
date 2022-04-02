using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float score;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple GameManager detected : intance destroyed");
            Destroy(this.gameObject);
        }
    }

    public float GetScore()
    {
        return score;
    }

    public void Score (float bulletValue)
    {
        score += bulletValue * Time.deltaTime;
    }

}
