using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPickup : MonoBehaviour
{
    public ItemObject pickupItem;
    public int pickupAmount = 1;

    public SpriteRenderer Key;

    private bool interacted;

    //public GameObject Player;

    void Start() {
        
        Key = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }
    void Awake() {
        interacted = false;
    }

   private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player entered zone");
            Key.enabled = true;
            if (Input.GetAxisRaw("Interact") > 0 && !interacted) {
                interacted = true;
                Debug.Log("Interacted with object");
            
                Player.instance.inventory.AddItemToInventory(pickupItem, pickupAmount);
                Destroy(gameObject);
            }
        }
    }

      private void OnTriggerExit(Collider collision) {
        if (collision.tag == "Player")
        {
            Key.enabled = false;
            
        }
    } 
}
