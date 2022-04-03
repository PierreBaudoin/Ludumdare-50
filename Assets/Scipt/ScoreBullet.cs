using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBullet : MonoBehaviour
{
    public float velocity;
    private RectTransform rect, target;
    Workingroom room;
    private Image image;
    private float timer;
    private float phase1 = 1.5f;
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
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < phase1)
        {
            GoUp();
        }
        else if (timer < phase2){}
        else {
            GoToScoreBar();
            }

        }

    
    void GoUp (){
        this.transform.position += transform.up.normalized * velocity/2 * Time.deltaTime;
        
    }


    void GoToScoreBar() {
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
