using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<Room> rooms;


    //OnClick() => Open Room, shutoff the other one. 

    // Room.SetVisibility()
    // Room.?????
    public void LightOn (Room roomSelected){
        foreach (Room room in rooms){
            if (room != roomSelected) {roomSelected.SetVisibility(false);}
            else {roomSelected.SetVisibility(true);}    
        }
        
    }




    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
