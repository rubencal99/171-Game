using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private bool guarded = false;

    public void toggleGuarded() {
        this.guarded = !this.guarded;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(!guarded) {
        if(other.tag == "Player") {
            this.GetComponent<Collider2D>().isTrigger = true;
            this.transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("made contact with player");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if(other.tag == "Player") {
            this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
      // this.GetComponent<Collider2D>().isTrigger = false;
    //   this.transform.GetChild(0).gameObject.SetActive(true);

        }
    //    this.transform.parent.Find("Exit Collider").GetChild(0).gameObject.SetActive(true);
        Debug.Log("leaving collider");
    }
}
