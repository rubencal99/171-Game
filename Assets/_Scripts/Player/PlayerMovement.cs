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
    public CapsuleCollider collider;
    //public Vector2 oriCollider;

    protected void Start()
    {
        PlayerState = GetComponent<PlayerStateManager>();
        collider = GetComponent<CapsuleCollider>();
        //oriCollider = collider.size;
    }


    // this function integrates acceleration
    protected override float calculateSpeed(Vector3 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentVelocity += MovementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= MovementData.decceleration * Time.deltaTime;
        }
        return AdjustedStateSpeed();
    }

    protected float AdjustedStateSpeed()
    {
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
    public void dodge(Vector3 dodgeDirection) {
        if(PlayerSignaler.CallWhiskers()){
            Debug.Log("whiskers");
        }else if(PlayerSignaler.CallHookShot()){
            Debug.Log("Hook");
        }else{
            rigidbody.velocity = Vector2.zero; // set speed to zero
            rigidbody.velocity += (Vector3)(dodgeDirection * dodgeVelocity); // create dodge
            Debug.Log("Dodge Velocity: " + rigidbody.velocity);
        }
    
    }

    public void ResetSpeed()
    {
        currentVelocity = 0;
    }
}
