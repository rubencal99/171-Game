using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchUp : MonoBehaviour
{
   private void OnTriggerEnter(Collider collision)
   {
      if (collision.tag == "Player")
      {
          
           Player player = Player.instance;
           player?.Heal(1);
           popup popup = FindObjectOfType<popup>();
           if(popup)
           {
               popup.SetText("2 health");
               popup.ShowText();
           }

            Destroy(gameObject);
           
      }
   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.tag == "Player")
      {
          
           Player player = Player.instance;
           player?.Heal(1);
           popup popup = FindObjectOfType<popup>();
           if(popup)
           {
               popup.SetText("2 health");
               popup.ShowText();
           }
            Destroy(gameObject);
           
      }
   }
}
