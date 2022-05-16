using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(Player.instance.hasKey && collider.tag == "Player")
        {
            Debug.Log("Player can continue to next floor");
            FloorExit.instance.GetComponent<FloorExit>().CallLoadScene();
        }
        else
        {
            Debug.Log("Player still needs to find the key");
        }
    }

    void OnCollisionEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            if(collider.GetComponent<Player>().hasKey) {
                Debug.Log("Player can continue to next floor");
                FloorExit.instance.GetComponent<FloorExit>().CallLoadScene();
            }
        }
        else
        {
            Debug.Log("Player still needs to find the key");
        }
    }
}
