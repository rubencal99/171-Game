using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
       // if(!guarded) {
            if(other.tag == "Player") {
               // this.GetComponent<Collider2D>().isTrigger = true;
                //this.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("exited map");
                this.GetComponent<ExitGame>().doExitGame();
            }
     //   }
    }
}
