using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBullet : MonoBehaviour
{
    public float velocity;
    public RectTransform rect;
    private RectTransform target;
    private Vector3 rectTargetPosition;
    Workingroom room;
    private Image image;
    private float timer;
    private float phase2 = 3f;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        target = GameManager.instance.bulletTraget;
    }
    public void Init(Vector3 position, Workingroom room)
    {
        this.room = room;
        rect.position =  Camera.main.WorldToScreenPoint(position);
        rectTargetPosition = Camera.main.WorldToScreenPoint(target.transform.position);
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= phase2)
            GoToScoreBar();
    }
    
    void GoUp (){
        this.transform.position += transform.up.normalized * velocity/2 * Time.deltaTime;
    }


    void GoToScoreBar() {
        if(Mathf.Abs(rect.position.x - rectTargetPosition.x + rect.position.y - rectTargetPosition.y) <= 5f)
        {
            GameManager.instance.Score(5);
            room.PoolInBullet(this);
        }
        else
        {
            Vector3 direction = rectTargetPosition - this.transform.position;
            this.transform.position += direction.normalized * velocity * Time.deltaTime;
        }
    }
}
