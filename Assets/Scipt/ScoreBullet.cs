using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBullet : MonoBehaviour
{
    public float velocity;
    private RectTransform rect, target;
    Workingroom room;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        target = GameManager.instance.bulletTraget;
    }
    public void Init(Vector3 position, Workingroom room)
    {
        this.room = room;
        rect.position =  Camera.main.WorldToScreenPoint(position);
    }

    void Update()
    {
        if(Mathf.Abs(rect.position.x - target.position.x + rect.position.y - target.position.y) <= 5f)
        {
            GameManager.instance.Score(5);
            room.PoolInBullet(this);
        }
        else
        {
            Vector3 direction = target.transform.position - rect.transform.position;
            this.transform.position += direction.normalized * velocity * Time.deltaTime;
        }
    }
}