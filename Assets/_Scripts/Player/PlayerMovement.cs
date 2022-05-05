using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    // Modifyable field for Dodge
    // **************
    [SerializeField]
    protected float dodgeVelocity = 600;


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
        //collider.size = new Vector2(1.1f, 0.6f);
        Vector3 dodge_dir = dodgeDirection;
        dodge_dir.y += 1.0f;
        Debug.Log("Dodge Dir: " + dodge_dir * dodgeVelocity);
        //dodgeVelocity.z *= 1.5f;
        rigidbody.velocity = Vector3.zero; // set speed to zero
        rigidbody.velocity += (Vector3)(dodge_dir * dodgeVelocity); // create dodge
        rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3(1f, 1f, 1.2f));
        Debug.Log("Dodge Velocity: " + rigidbody.velocity);
        //Debug.Log("Collider size: " + collider.size);
        //Debug.Log("Original height and width:" + oriCollider);
        //collider.size = oriCollider;
    }

    public void CollisionsOff() {
        rigidbody.Sleep();
        rigidbody.detectCollisions  = false;
    }

    public void CollisionsOn() {
         rigidbody.WakeUp();
        rigidbody.detectCollisions  = true;
    }

    public void ResetSpeed()
    {
        currentVelocity = 0;
    }
}
