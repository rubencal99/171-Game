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

    protected void Start()
    {
        PlayerState = GetComponent<PlayerStateManager>();
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
        if(PlayerState.ProneState.standing == false)
        {
            return Mathf.Clamp((currentVelocity), 0, MovementData.maxProneSpeed);
        }
        // Returns velocity between 0 and maxSpeed
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }

    //********** Dodge function
    // Should player be able to dodge when not moving??
    public void dodge(Vector2 dodgeDirection) {
        rigidbody2D.velocity = Vector2.zero; // set speed to zero
        rigidbody2D.velocity += dodgeDirection * dodgeVelocity; // create dodge
        Debug.Log("Dodge Velocity: " + rigidbody2D.velocity);
    }

    public void ResetSpeed()
    {
        currentVelocity = 0;
    }
}
