using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class _BasePassive : MonoBehaviour
{
    [SerializeField]
    protected float duration;

    void OnTriggerEnter(Collider collision){
        if (collision.CompareTag("Player")){
            // Here we instantiate a visual effect

            // Here we disable the object so it's invisible once picked up

            // Here we execute the Pickup function that accesses player stats
            StartCoroutine(Pickup(collision));
            
        }
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player")){
            // Here we instantiate a visual effect

            // Here we disable the object so it's invisible once picked up

            // Here we execute the Pickup function that accesses player stats
            StartCoroutine(Pickup(collision));
            
        }
    }

    public abstract IEnumerator Pickup(Collider player);

    public abstract IEnumerator Pickup(Collision player);
}
