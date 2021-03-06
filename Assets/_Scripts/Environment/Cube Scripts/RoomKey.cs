using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomKey : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<RoomDoor>())
        {
            // Clear room
            Debug.Log("Puzzle complete");
            if( this.GetComponent<DestructibleBox>() != null)
            this.GetComponent<DestructibleBox>().OnDie?.Invoke();
            Destroy(gameObject);
        }
    }
}
