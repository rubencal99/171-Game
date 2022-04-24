using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<RoomKey>())
        {
            // Clear room
            Debug.Log("Puzzle complete");
            transform.parent.GetComponent<RoomClearCheck>().checkIfClear();
            Destroy(gameObject);
        }
    }
}
