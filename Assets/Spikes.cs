using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisonEnter2D(Collider2D col){
        Debug.Log("OnCollision2d hit!");
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("OnTriggerEnter2d hit!");
        Destroy(this.gameObject);
    }
}
