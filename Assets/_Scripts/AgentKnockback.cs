using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentKnockback : MonoBehaviour
{
    public Rigidbody rb;
    public AgentMovement agentMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agentMovement = GetComponent<AgentMovement>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with object");
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") && agentMovement.knockback)
        {
            BounceAgent(collision);
        }
    }

    void BounceAgent(Collision collision)
    {
        //Debug.Log("In bounce agent");
        Vector3 inNormal = collision.contacts[0].normal;
        Vector3 direction = agentMovement.knockbackDirection;
        //Debug.Log("Prev KDirection: " + direction);
        Vector3 newDirection = Vector3.Reflect(direction, inNormal);
        //Debug.Log("New KDirection: " + newDirection);
        agentMovement.Knockback(1f, agentMovement.knockbackPower/2, newDirection);
        /*agentMovement.knockbackDirection = newDirection;
        Debug.Log("New KDirection: " + newDirection);
        Vector3 k = -newDirection;
        rb.AddForce(k.x, 0, k.z, ForceMode.Impulse);*/
    }
}
