using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletPickup : MonoBehaviour
{
    private PlayerWeapon weapon;
      [field: SerializeField]
    public UnityEvent OnPickup { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
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
            popup.SetText("ammo");
            popup.ShowText();
             Destroy(gameObject);
        }
    }
}
