using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogofwar : MonoBehaviour
{
    // Start is called before the first frame update

    public bool player_detected = false;
    public LayerMask m_LayerMask;
   void OnTriggerEnter(Collider collision) {
        if(collision.tag == "Player") {
            player_detected = true;
        // Debug.Log("detected player");
            clear_fog();

        }
    }

    void clear_fog() {

         Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.parent.position, transform.parent.localScale / 2, Quaternion.identity, m_LayerMask);
        foreach(Collider hit in hitColliders) {
             if (hit.gameObject.GetInstanceID() == gameObject.GetInstanceID())
                continue;
          // Debug.Log("Hit : " + hit.name );
            if(hit.tag == "minimapblocker")
                Destroy(hit.gameObject);
        }
        Destroy(this.gameObject);
    }
}
