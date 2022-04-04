using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workingroom : Room
{
    public float powerFactor = 0.5f;

    public float currentScore = 0;
    public int bulletValue = 5;
    public GameObject bulletPrefab;
    public List<ScoreBullet> bulletPool;
    [SerializeField] private string hungerName, fatigueName, hygieneName, moralName;

    public override void UseRoomUpdate(Character character)
    {
        currentScore += GetProductivity(character) * Time.deltaTime;
    }

    public float GetProductivity(Character character)
    {
        float productivity;

        float hunger = GetStat(character.stats, hungerName).actualValue;
        float fatigue = GetStat(character.stats, fatigueName).actualValue;
        float hygiene = GetStat(character.stats, hygieneName).actualValue;
        float moral = GetStat(character.stats, moralName).actualValue;

        //productivity = Mathf.Pow(hunger * fatigue * hygiene * moral * hunger * fatigue * hygiene * moral, powerFactor) / powerFactor;
        productivity = character.characterData.GetProductivity();
        Debug.LogWarning (character.characterData.characterName + " => " + productivity);
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
            b.transform.SetParent(GameManager.instance.mainCanvas);
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
