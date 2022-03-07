using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryCollider : MonoBehaviour
{
    public BoxCollider boxCol;

    
    // Start is called before the first frame update
    public void Start() {

       // boxCol = GetComponent<BoxCollider2D>();
        RoomNode room = this.transform.parent.GetComponent<RoomNode>();
        this.gameObject.transform.localScale = new Vector3((float)room.length - 1f, (float)room.width - 1f, 1f);
    }
    
    private bool guarded = false;

    public void toggleGuarded() {
        this.guarded = !this.guarded;
    }
    void OnTriggerEnter2D(Collider2D other) {
       // if(!guarded) {
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
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
