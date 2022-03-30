using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnockback : MonoBehaviour
{   
    [SerializeField] private float knockbackStrength;
    private void OnTriggerEnter(Collision collision) {
        Debug.Log("Bullet collided");
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        
        if (rb != null) {
            Debug.Log("In Dive velocity");
            Vector3 direction = collision.transform.position - transform.position;

            rb.AddForce(direction * knockbackStrength, ForceMode.Impulse);
        }
    }
}
