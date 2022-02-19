using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnockback : MonoBehaviour
{   
    [SerializeField] private float knockbackStrength;
    private void OnTriggerEnter2D(Collision collision) {
        Debug.Log("Bullet collided");
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        
        if (rb != null) {
            Debug.Log("In Dive velocity");
            Vector2 direction = collision.transform.position - transform.position;

            rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        }
    }
}
