using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    // Modifyable field for Dodge
    // **************
    [SerializeField]
    protected float dodgeVelocity = 600;

    float drag = 2f;
    protected PlayerStateManager PlayerState;
    public CapsuleCollider collider;
    //public Vector2 oriCollider;

    protected void Start()
    {
        PlayerState = GetComponent<PlayerStateManager>();
        collider = GetComponent<CapsuleCollider>();
        //oriCollider = collider.size;
    }

    public virtual void MoveAgent(Vector3 movementInput)
    {
        // rigidbody2D.velocity = movementInput.normalized * currentVelocity;
        if (movementInput.magnitude > 0)
        {
            /* // Use this if we want car-like deceleration
            if (Vector2.Dot(movementInput.normalized, movementDirection) < 0)
                currentVelocity = 0;
            */
            //Debug.Log("Movement input: " + movementInput);
            movementDirection = movementInput.normalized;
        }
        /*else{
            movementDirection = Vector2.zero;
        }*/
        
        currentVelocity = calculateSpeed(movementInput) * Passives.SpeedMultiplier * PlayerSignaler.SetMovementSpeed();
        
        //Debug.Log("Current velocity: " + currentVelocity);
        if(this.GetComponentInChildren<AgentAnimations>() != null)
             this.GetComponentInChildren<AgentAnimations>().SetWalkAnimation(movementInput.magnitude > 0);
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
        if(Player.instance.grabbing && Player.instance.grabbedObject != null)
        {
            Player.instance.grabbedObject.GetComponent<Grabbable>().AdjustSpeed(rigidbody.velocity);
            return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed / Player.instance.grabbedObject.GetComponent<Grabbable>().mass);
        }
        // Returns velocity between 0 and maxSpeed
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }

    //********** Dodge function
    // Should player be able to dodge when not moving??
    public void dodge(Vector3 dodgeDirection) {
        //collider.size = new Vector2(1.1f, 0.6f);
        drag = PlayerState.DiveState.CalculateDrag();
        Vector3 dodge_dir = dodgeDirection;
        dodge_dir.y += 1.0f;
        Debug.Log("Dodge Dir: " + dodge_dir * dodgeVelocity);
        rigidbody.velocity = Vector3.zero; // set speed to zero
        rigidbody.velocity += (Vector3)(dodge_dir * dodgeVelocity); // create dodge
        rigidbody.drag = drag;
        //rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3(1f, 1f, 1.2f));
        Debug.Log("Dodge Velocity: " + rigidbody.velocity);
        //Debug.Log("Collider size: " + collider.size);
        //Debug.Log("Original height and width:" + oriCollider);
        //collider.size = oriCollider;
    }

    protected virtual void FixedUpdate()
    {   
        if(knockback)
        {
            knockbackTimer -= Time.deltaTime;
            if(knockbackTimer <= 0)
            {
                knockback = false;
            }
            // Vector2 k = -knockbackDirection * knockbackPower;
            // rigidbody2D.AddForce(k, ForceMode2D.Impulse);
            //rigidbody2D.velocity += k;
            return;
        }
        OnVelocityChange?.Invoke(currentVelocity);
        if(rigidbody != null && !knockback) {
         rigidbody.velocity = currentVelocity * movementDirection.normalized;
         rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3 (1f, 1f, 1.65f));
        }
        if(Player.instance.grabbedObject != null)
        {
            Player.instance.grabbedObject.GetComponent<Grabbable>().AdjustPosition();
        }
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
