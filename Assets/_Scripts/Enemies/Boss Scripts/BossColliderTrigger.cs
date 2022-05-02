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
    public SquidMovement squidMovement;
    public EnemyBrain enemyBrain;
    public LayerMask layerMask;
    public AIMovementData aiMovementData;
    RaycastHit hit;

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

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Boss collided with somthing");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Debug.Log("Boss collided with obstacle");
            if(SquidBoss.inCyclone)
            {
                Debug.Log("Before bounce boss");
                BounceBoss(collision);
            }
        }
        
    }

    void BounceBoss(Collision collision)
    {
        /*Vector3 inNormal = collision.contacts[0].normal;
        Vector3 newDirection = Vector3.Reflect(squidMovement.movementDirection, inNormal);
        newDirection.y = 0;*/

        Vector3 newDirection = enemyBrain.Target.transform.position - transform.position;
        newDirection.y = 0;

        Debug.Log("In Bounce Boss");
        Debug.Log("CURRENT Direction: " + squidMovement.movementDirection);
        Debug.Log("New Direction: " + newDirection);
        squidMovement.movementDirection = newDirection;
        SquidBoss.cycloneDirection = newDirection;

         Debug.DrawRay(transform.position, SquidBoss.cycloneDirection);
        hit = new RaycastHit();
        Physics.Raycast(transform.position, SquidBoss.cycloneDirection, out hit, 100, layerMask);
        aiMovementData.PointOfInterest = hit.transform.position;
        aiMovementData.PointerPosition = hit.transform.position;
    }
}
