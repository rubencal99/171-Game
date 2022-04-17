using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderListener : MonoBehaviour
 {
     public Collider collider;
     void Awake()
     {
         if (collider.gameObject != gameObject)
         {
             ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
             cb.Initialize(this);
         }
     }
     public void OnCollisionEnter(Collision collision)
     {
         GetComponent<CollidesWithWallDecision>().hasCollided = true;
     }
     public void OnTriggerEnter(Collider other)
     {
         GetComponent<CollidesWithWallDecision>().hasCollided = true;
     }
 }
