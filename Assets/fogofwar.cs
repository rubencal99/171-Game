using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogofwar : MonoBehaviour
{
    // Start is called before the first frame update
   void OnTriggerEnter(Collider collision) {
        if(collision.tag == "Player") {
         Debug.Log("detected player");
            Destroy(this.gameObject);
        }
    }

    void clear_fog() {
        Destroy(this.gameObject);
    }
}
