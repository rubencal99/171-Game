using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class _BasePassive : MonoBehaviour
{
    [SerializeField]
    protected float duration;

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            // Here we instantiate a visual effect

            // Here we disable the object so it's invisible once picked up

            // Here we execute the Pickup function that accesses player stats
            StartCoroutine(Pickup(collision));
            
        }
    }

    public abstract IEnumerator Pickup(Collider2D player);
}
