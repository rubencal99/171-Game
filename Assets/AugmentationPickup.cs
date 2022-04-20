using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentationPickup : MonoBehaviour
{
    public AugmentationItemSO augmentation;

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided with object " + col.tag);
        if (col.tag == "Player")
        {
            Player.instance.inventory.AddItemToInventory(augmentation, 1);
            Destroy(gameObject);
        }
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
