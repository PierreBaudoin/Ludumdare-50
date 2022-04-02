using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workingroom : Room
{
    public float currentScore = 0;
    public int characterindex = 0;
    public int bulletValue = 5;
    public GameObject bulletPrefab;
    public List<ScoreBullet> bulletPool;
    [SerializeField] private string hungerName, fatigueName, hygieneName, moralName;

    public override void UseRoomUpdate(Character character)
    {
        currentScore += GetProductivity(character);
    }

    private float GetProductivity(Character character)
    {
        float productivity;

        float hunger = GetStat(character.stats, hungerName).actualValue;
        float fatigue = GetStat(character.stats, fatigueName).actualValue;
        float hygiene = GetStat(character.stats, hygieneName).actualValue;
        float moral = GetStat(character.stats, moralName).actualValue;

        productivity = Mathf.Pow(hunger * fatigue * hygiene * moral * hunger * fatigue * hygiene * moral, 1/5);
        Debug.Log(productivity + " : " + hunger + " - " + fatigue + " - " + hygiene + " - " + moral);

        return productivity;
    }

    void Update()
    {
        if(currentScore >= bulletValue)
        {
            ScoreBullet b = PoolOutBullet();
            currentScore -= bulletValue;
        }
    }

    private ScoreBullet PoolOutBullet()
    {
        ScoreBullet b;

        if(bulletPool.Count > 0)
        {
             b = bulletPool[0];
        }
        else
        {
            b = Instantiate(bulletPrefab).GetComponent<ScoreBullet>();
        }

        b.gameObject.SetActive(true);
        
        if(characterindex >= charactersInRoom.Count)
        {
            characterindex = 0;
        }
        
        b.Init(charactersInRoom[characterindex].transform.position, this);
        characterindex ++;
        
        return b;
    }

    public void PoolInBullet(ScoreBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Add(bullet);
    }
}
