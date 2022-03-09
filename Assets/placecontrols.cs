using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placecontrols : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject Player;

   public void SetPosition() {
        this.transform.position = Player.transform.position;
   }
}
