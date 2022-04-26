using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
   private void OnTriggerEnter(Collider collision)
   {
      if (collision.tag == "Player")
      {
          
           Player player = FindObjectOfType<Player>();
           player?.Heal(10);
           popup popup = FindObjectOfType<popup>();
            popup.SetText("10 health");
            popup.ShowText();
             Destroy(gameObject);
           
      }
   }
}
