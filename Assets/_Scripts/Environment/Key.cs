using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("Player has aquired key");
            Player.instance.hasKey = true;
            Destroy(gameObject);
        }
    }
}
