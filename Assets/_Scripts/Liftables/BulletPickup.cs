using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletPickup : MonoBehaviour
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
                weapon.Fill();
            }
            popup popup = FindObjectOfType<popup>();
            popup.SetText("ammo refill");
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
                weapon.Fill();
            }
            popup popup = FindObjectOfType<popup>();
            popup.SetText("ammo refill");
            popup.ShowText();
             Destroy(gameObject);
        }
    }
}
