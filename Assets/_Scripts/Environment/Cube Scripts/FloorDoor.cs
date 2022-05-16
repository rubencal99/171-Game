using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    // void OnTriggerEnter(Collider collider)
    // {
        void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("Player has aquired key");
            Player.instance.hasKey = true;
            Destroy(gameObject);    
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(Player.instance.hasKey)
                FloorExit.instance.GetComponent<FloorExit>().CallLoadScene();
        }
    }
    //     Debug.Log("Collider tag = " + collider.tag);
    //     if(collider.tag == "Player")
    //     {
    //         if(collider.GetComponent<Player>().hasKey) {
    //         Debug.Log("Player can continue to next floor");
    //         FloorExit.instance.GetComponent<FloorExit>().CallLoadScene();
    //         }
    //     }
    //     // else
    //     // {
    //     //     Debug.Log("Player still needs to find the key");
    //     // }
    // }

    // void OnCollisionEnter(Collider collider)
    // {
    //     if(collider.tag == "Player")
    //     {
    //         if(collider.GetComponent<Player>().hasKey) {
    //             Debug.Log("Player can continue to next floor");
    //             FloorExit.instance.GetComponent<FloorExit>().CallLoadScene();
    //         }
    //     }
    //     // else
    //     // {
    //     //     Debug.Log("Player still needs to find the key");
    //     // }
    // }
}
