using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BossColliderTrigger : MonoBehaviour
{
    public float knockbackDuration;
    public float knockbackPower;
    public Vector2 direction;
    public int damage;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            direction = (transform.position - other.transform.position).normalized;
            Debug.Log("In player knockback");
            other.transform.GetComponent<PlayerMovement>().Knockback(knockbackDuration, knockbackPower, direction);
            other.transform.GetComponent<Player>().GetHit(damage, gameObject);
        }
    }
}
