using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.tag == "Player")
      {
           Destroy(gameObject);
           Player player = FindObjectOfType<Player>();
           player?.Heal();
      }
   }
}
