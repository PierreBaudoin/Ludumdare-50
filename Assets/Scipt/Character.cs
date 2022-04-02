using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool isDragged = false;
    private  Room oldRoom;
    public Stat[] stats;
    private EventData[] possibleEvents;
    private string[] traitList;
    private Transform oldTransform;
    private Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        ///Get Data from CharacterData file
    
        ///Get Local Events from EventData file

        oldPos = this.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        ActualizeStats();
        //Roll ?
        CheckEvents(possibleEvents);
        if (isDragged){Drag();}
        //Productivity
    }

    void Drag (){
        if (!isDragged){
            foreach (Character o in GameObject.FindObjectsOfType<Character>()){
                if (o.isDragged && o != this){
                    return;
                }
            }
            isDragged = true;
        }

        LayerMask layerMask = LayerMask.GetMask("Floor");
        RaycastHit hit;
        Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));

        if (Physics.Raycast(pos, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)){
            this.transform.position = hit.point;
        }

        if (Input.GetMouseButtonUp(0)){
            Drop();
        }   
    }

    void Drop (){
        isDragged = false;
       
        LayerMask layerMask = LayerMask.GetMask("Room");
        RaycastHit hit;
        Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mousePos.z));

        //Raycast to get the Room's Script;
        if (Physics.Raycast(pos, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            //Si le character entre dans une nouvelle room ou si l'ancienne room est set. 
            if (oldRoom != hit.collider.gameObject.GetComponent<Room>() || oldRoom != null){ //Si le joueur drop le character dans une room différente...
                if (oldRoom != null){ oldRoom?.RemoveCharacter(this, oldTransform);}
               
                Room newRoom = hit.collider.gameObject.GetComponent<Room>();

                if (!newRoom.IsRoomFull()){ // Si la nouvelle room n'est pas pleine
                    this.oldTransform = newRoom.AddCharacter(this);
                    this.transform.position = oldTransform.position;
                    oldRoom = newRoom;
                }
                else{  // Si la nouvelle room est pleine 
                    if (oldTransform != null){
                        this.transform.position = oldTransform.position;
                    } else
                    {
                        this.transform.position = oldPos;
                    }
                    
                }
            } 
            else //Si le joueur drop le character dans la même room ou un endroit vide (pas une room)
            {
                if (oldTransform != null){
                        this.transform.position = oldTransform.position;
                    } else
                    {
                        this.transform.position = oldPos;
                    }
                    
            }
        } 
    }

    //Drag And Drop
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0)){
            //Follow Mouse
            //Enter pick up state
            Drag(); 
        }
    }

    

    private void CheckEvents(EventData[] events)
    {/*
        foreach(EventData ev in events)
        {
            foreach (TriggerCondition trig in ev.possibleTriggers)
            {
                if (trig.Check(traitList, stats))
                {
                    foreach (TargetEffectPair pair in ev.targetEffectPairs)
                    {
                        pair.Play();
                    }
                }
                
            }
        }*/
    }

    private void ActualizeStats()
    {
        /*
        foreach(Stat stat in stats)
        {
            float var = stat.actualValue - (stat.depressionRate * Time.deltaTime);
            stat.Reduce(var);
        }*/
    }
}

public class Roll
{

}

