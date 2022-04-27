using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPickup : MonoBehaviour
{
    public ItemObject pickupItem;
    public int pickupAmount = 1;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided with object " + col.tag);
        if (col.tag == "Player")
        {
            Player.instance.inventory.AddItemToInventory(pickupItem, pickupAmount);
            Destroy(gameObject);
        }
    } 
}
