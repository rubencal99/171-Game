using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EntryCollider : MonoBehaviour
{
    public BoxCollider boxCol;

    RoomNode room;

    
    // Start is called before the first frame update
    public void Start() {

       // boxCol = GetComponent<BoxCollider2D>();
        room = this.transform.parent.GetComponent<RoomNode>();
        this.gameObject.transform.localScale = new Vector3((float)room.length, 5, (float)room.width);
    }
    
    private bool guarded = false;

    public void toggleGuarded() {
        this.guarded = !this.guarded;
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
       // if(!guarded) {
           //Debug.Log("Just entered room");
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                //GraphUpdater.SetNewBounds(GetComponent<Collider2D>().bounds);
            //this.GetComponent<Collider2D>().isTrigger = false;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
           //  Debug.Log("leaving collider");
            }
     //   }
    }

    // void OnTriggerExit2D(Collider2D other) {

    //     if(other.tag == "Player") {
           
    //     }
    // //    this.transform.parent.Find("Exit Collider").GetChild(0).gameObject.SetActive(true);
       
    // }
}
