using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workingroom : Room
{
    public float currentScore = 0;
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

        float hunger = GetStat(character.stats, hungerName).actualValue /100;
        float fatigue = GetStat(character.stats, fatigueName).actualValue /100;
        float hygiene = GetStat(character.stats, hygieneName).actualValue /100;
        float moral = GetStat(character.stats, moralName).actualValue /100;

        productivity = Mathf.Pow(hunger * fatigue * hygiene * moral * hunger * fatigue * hygiene * moral, 1/10) / 10;

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
             bulletPool.Remove(b);
        }
        else
        {
            b = Instantiate(bulletPrefab).GetComponent<ScoreBullet>();
            b.transform.SetParent(GameManager.instance.bulletTraget.parent);
        }

        b.Init(slotsTransform[Random.Range(0, slotsTransform.Length)].position, this);
        b.gameObject.SetActive(true);
        
        return b;
    }

    public void PoolInBullet(ScoreBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Add(bullet);
    }
}
