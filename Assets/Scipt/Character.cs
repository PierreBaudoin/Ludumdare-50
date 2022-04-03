using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Character : MonoBehaviour
{
    public Stat[] stats;
    public CharacterData characterData;
    public Transform model;
    private bool isDragged = false;
    private  Room currentRoom;
    private Transform currentTransform;
    private EventData[] possibleEvents;
    private string[] traitList;
    private Renderer[] renderers;
    private Collider collider;

    void Start()
    {
        stats = GetStats();
        foreach(Stat s in stats)
        {
            s.actualValue = s.maxValue;
        }
        GameManager.instance.characters.Add(this);
        renderers = model.GetComponentsInChildren<Renderer>();
        collider = GetComponent<Collider>();

        JoinRoom(GameManager.instance.workingroom);
    }

    void OnDestroy()
    {
        GameManager.instance.characters.Remove(this);
    }

    public Stat[] GetStats()
    { /*
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(CharacterData).Name);
        CharacterData[] a = new CharacterData[guids.Length];

        for(int i =0;i<guids.Length;i++) 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<CharacterData>(path);
        }
 
        return a[0].stats;*/
        return characterData.stats;
     }

    void Update()
    {
        ActualizeStats();
        if (isDragged){
            Drag();
        }
    }

#region drag/drop
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
            Room newRoom = hit.transform.gameObject.GetComponentInParent<Room>();
            //Si même room
            if (newRoom == currentRoom){
                StartCoroutine(TravelBack(currentTransform.position));
                return;
            }
            //Si room différente
            if (newRoom != currentRoom){
                if (!newRoom.IsRoomFull()){ //Et qu'elle n'est pas pleine
                    if (currentRoom != null)
                    {
                        currentRoom.EndUseRoom(this);
                        currentRoom.RemoveCharacter(this);
                    }
                    
                    JoinRoom(newRoom);
                    return;
                }
                else //Si la room est pleine 
                {   
                    StartCoroutine(TravelBack(currentTransform.position));
                }
            }
            
        } else //Si pas de room 
        {
            Debug.LogError("Character is in no room.");
            if (currentRoom != null) {currentRoom.RemoveCharacter(this);}
            currentRoom = null;
            StartCoroutine(TravelBack(hit.point));
        } 
    }

    IEnumerator TravelBack(Vector3 targetPosition){
        while (Vector3.Distance (this.transform.position, targetPosition) > 0.2f){
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 20f * Time.deltaTime);
            yield return null; 
        }
    }

    private void JoinRoom(Room room)
    {
        this.currentTransform = room.AddCharacter(this);
        StartCoroutine(TravelBack(currentTransform.position));
        room.StartUseRoom(this);
        currentRoom = room;
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
#endregion

    private void ActualizeStats()
    {
        currentRoom?.UseRoomUpdate(this);
        foreach(Stat s in stats)
        {
            s.Reduce(s.depressionRate * Time.deltaTime);
        }
    }

    public void Disappear()
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
        collider.enabled = false;
    }

    public void Appear()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
        collider.enabled = true;
    }
}