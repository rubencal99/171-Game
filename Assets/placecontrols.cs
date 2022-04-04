using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placecontrols : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject Player;

   public void SetPosition(Vector3 pos) {
        this.transform.localPosition = pos;
   }
}
