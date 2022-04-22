using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(Player.instance.hasKey)
        {
            Debug.Log("Player can continue to next floor");
            FloorExit.instance.GetComponent<FloorExit>().CallLoadScene();
        }
        else
        {
            Debug.Log("Player still needs to find the key");
        }
    }
}
