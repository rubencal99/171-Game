using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    private PlayerWeapon weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
            Player player = FindObjectOfType<Player>();
            weapon = player.gameObject.GetComponentInChildren<PlayerWeapon>();
            if (weapon != null)
            {
                weapon.Fill();
            }
        }
    }
}
