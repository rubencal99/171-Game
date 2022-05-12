using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCube : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<EmpCube>())
        {
            // Clear room
            Debug.Log("EC has collided with EMP");
            //transform.parent.GetComponent<RoomClearCheck>().checkIfClear();
            Destroy(gameObject);
        }
    }
}
