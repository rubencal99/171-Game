using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EntryCollider : MonoBehaviour
{
    public BoxCollider boxCol;

    public float boundsOffset = 0.5f;

    RoomNode room;

    
    // Start is called before the first frame update
    public void Start() {

       // boxCol = GetComponent<BoxCollider2D>();
        room = this.transform.parent.GetComponent<RoomNode>();
        this.gameObject.transform.localScale = new Vector3((float)room.length, 5, (float)room.width);
        // List<Vector2> corner_points = new List<Vector2>()
        // {
        //     room.bottomLeftCorner,
        //     room.topLeftCorner,
        //     room.topRightCorner,
        //     room.bottomRightCorner
        // };
        // edge = GetComponentInChildren<EdgeCollider2D>();
        // edge.SetPoints(corner_points);
        // Debug.Log(edge);
    }
    
    public bool guarded = false;

    public void toggleGuarded() {
        this.guarded = !this.guarded;
    }

    public void setGuarded(bool val) {
        this.guarded = val;
    }
    /*void OnTriggerEnter2D(Collider2D other) {
       // if(!guarded) {
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                GraphUpdater.SetNewBounds(GetComponent<Collider2D>().bounds);
            //this.GetComponent<Collider2D>().isTrigger = false;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
           //  Debug.Log("leaving collider");
            }
     //   }
    }*/

    void OnTriggerEnter(Collider other) {
        if(!guarded) {
           //Debug.Log("Just entered room");
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                //GraphUpdater.SetNewBounds(GetComponent<Collider2D>().bounds);
            //this.GetComponent<Collider2D>().isTrigger = false;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            //this.gameObject.SetActive(false);
           //  Debug.Log("leaving collider");
            }
       }
    }

    private void FixedUpdate() {
        Vector3 newLocation = Player.instance.transform.localPosition;
        Vector2 minPosition = room.bottomLeftCorner;
        minPosition.x -= boundsOffset; minPosition.y -= boundsOffset;
        Vector2 maxPosition = room.topRightCorner;
        maxPosition.x += boundsOffset; maxPosition.y += boundsOffset;
        if(guarded) {
            
         if (newLocation.x > maxPosition.x)
         {
             newLocation.x = maxPosition.x;
         }
         else if (newLocation.x < minPosition.x)
         {
             newLocation.x = minPosition.x;
         }
         if (newLocation.z > maxPosition.y)
         {
             newLocation.z = maxPosition.y;
         }
         else if (newLocation.z < minPosition.y)
         {
             newLocation.z = minPosition.y;
         }    

        Player.instance.transform.localPosition = newLocation;

        }
    }
     void OnTriggerExit(Collider other)
    { 
        if(guarded) {    
            if(other.gameObject.tag == "Player") {
                GetComponent<BoxCollider>().enabled = true;
            } 
        }
    }

    // void OnTriggerExit2D(Collider2D other) {

    //     if(other.tag == "Player") {
           
    //     }
    // //    this.transform.parent.Find("Exit Collider").GetChild(0).gameObject.SetActive(true);
       
    // }
}
