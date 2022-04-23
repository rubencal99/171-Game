using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
