using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PassiveItem : MonoBehaviour
{

    public string name;
    public string description;

    public PassiveType type;

    //intensity = percentage of effect (0 - 100)
     [Range(-100.0f,100.0f)]
    public float intensity;

    public bool applied = false;


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.tag == "Player") {
            PlayerInventory inventory = collision.GetComponentInChildren<PlayerInventory>();
            Debug.Log("picked up passive upgrade");
            inventory.items.Add(this);
            this.transform.parent = inventory.transform;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Collider2D>().enabled = false;           
           // Destroy(gameObject);
            inventory.applyEffects();
        }
    }
}


public enum PassiveType { 
   HP, MoveSpeed, ReloadSpeed, Recoil, FireRate
}
