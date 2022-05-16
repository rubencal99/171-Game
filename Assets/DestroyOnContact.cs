using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

     public void selfdestruct() {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "minimaptracker")
            Destroy(gameObject);
    }

    public void OnTriggerStay(Collider other) {
        if(other.tag == "minimaptracker")
            Destroy(gameObject);
    }
}
