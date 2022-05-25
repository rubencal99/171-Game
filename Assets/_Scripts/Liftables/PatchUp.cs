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
           int healRand = Random.Range(1, 3);
           player?.Heal(healRand);
           popup popup = FindObjectOfType<popup>();
           if(popup)
           {
               popup.SetText("Patch Up");
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
           int healRand = Random.Range(1, 3);
           player?.Heal(healRand);
           popup popup = FindObjectOfType<popup>();
           if(popup)
           {
               popup.SetText("Patch Up");
               popup.ShowText();
           }
            Destroy(gameObject);
           
      }
   }
}
