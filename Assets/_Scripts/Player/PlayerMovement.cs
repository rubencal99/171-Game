using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    // Modifyable field for Dodge
    // **************
    [SerializeField]
    protected float dodgeVelocity = 200;

    protected PlayerStateManager PlayerState;
    public CapsuleCollider2D collider;
    public Vector2 oriCollider;

    protected void Start()
    {
        PlayerState = GetComponent<PlayerStateManager>();
        collider = GetComponent<CapsuleCollider2D>();
        oriCollider = collider.size;
    }


    // this function integrates acceleration
    protected override float calculateSpeed(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentVelocity += MovementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= MovementData.decceleration * Time.deltaTime;
        }
        // Check if player is dodging
        // Debug.Log("Diving = " + PlayerState.DiveState.diving);
        if (PlayerState.DiveState.diving == true) {
            Debug.Log("In Dive velocity");
            return Mathf.Clamp((currentVelocity * dodgeVelocity), 0, MovementData.maxDodgeSpeed);
        }
        // Returns velocity between 0 and maxSpeed
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }

    //********** Dodge function
    // Should player be able to dodge when not moving??
    public void dodge(Vector2 dodgeDirection) {
        collider.size = new Vector2(1.1f, 0.6f);
        rigidbody2D.velocity = Vector2.zero; // set speed to zero
        rigidbody2D.velocity += dodgeDirection * dodgeVelocity; // create dodge
        Debug.Log("Dodge Velocity: " + rigidbody2D.velocity);
        Debug.Log("Collider size: " + collider.size);
        Debug.Log("Original height and width:" + oriCollider);
        collider.size = oriCollider;
    }

    public void ResetSpeed()
    {
        currentVelocity = 0;
    }
}
