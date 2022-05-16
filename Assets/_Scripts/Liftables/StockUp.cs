using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StockUp : MonoBehaviour
{
    private PlayerWeapon weapon;
      [field: SerializeField]
    public UnityEvent OnPickup { get; set; }

    private void OnTriggerEnter(Collider collision)
    {   
        if(collision.tag == "Player")
        {
           
            Player player = FindObjectOfType<Player>();
            weapon = player.gameObject.GetComponentInChildren<PlayerWeapon>();
            if (weapon != null)
            {
                weapon.Supply();
            }
            popup popup = FindObjectOfType<popup>();
            popup.SetText("ammo stock up");
            popup.ShowText();
             Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if(collision.gameObject.tag == "Player")
        {
           
            Player player = FindObjectOfType<Player>();
            weapon = player.gameObject.GetComponentInChildren<PlayerWeapon>();
            if (weapon != null)
            {
                weapon.Supply();
            }
            popup popup = FindObjectOfType<popup>();
            popup.SetText("ammo stock up");
            popup.ShowText();
             Destroy(gameObject);
        }
    }
}
