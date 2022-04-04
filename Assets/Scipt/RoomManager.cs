using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<Room> rooms;


    void Start()
    {
        foreach(Room r in rooms)
        {
            if (r.GetType() == typeof(Workingroom))
            {
                r.SetVisibility(true);
            }
        }
    }

    //OnClick() => Open Room, shutoff the other one. 

    // Room.SetVisibility()
    // Room.?????
    public void LightOn (Room roomSelected){
        foreach (Room room in rooms){
            if (room != roomSelected) {room.SetVisibility(false);}
            else {
                room.SetVisibility(true);
                if(room.tutoView != null)
                {
                    TutorialManager.instance.ShowTutoView(room.tutoView.GetComponent<TutorialViewController>());
                }
            }
        }
    }




    void Update()
    {
        
    }
}
