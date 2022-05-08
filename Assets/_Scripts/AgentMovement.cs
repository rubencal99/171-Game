using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Script requires RigidBody attached to Player object
[RequireComponent(typeof(Rigidbody))]
public class AgentMovement : MonoBehaviour
{
    public Rigidbody rigidbody;

    protected PlayerPassives Passives;

    // MovementData is a scriptable object that contains data regarding movement
    [field: SerializeField]
    public MovementDataSO MovementData { get; set; }

    // This is only serialized for debugging purposes
    [SerializeField]
    public float currentVelocity = 0;
    [SerializeField]
    public Vector3 movementDirection;

    // This passes currentVelocity to AgentAnimations.AnimatePlayer
    // Hence SerializeField
    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

    public bool knockback;
    public float knockbackPower;
    public float knockbackTimer;
    public Vector3 knockbackDirection;


    protected void Awake()
    {
        // Grabs RigidBody that the script is attached to
        rigidbody = GetComponent<Rigidbody>();

        // Grabs PlayerPassives script
        Passives = GetComponent<PlayerPassives>();

        // Resets MovementData from previous plays
        // MovementData.maxSpeed = DefaultSpeed;
    }

    // Takes Vector2 from AgentInput OnMovementKeyPressed
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
        
        currentVelocity = calculateSpeed(movementInput) * Passives.SpeedMultiplier;
        if(this.GetComponentInChildren<AgentAnimations>() != null)
             this.GetComponentInChildren<AgentAnimations>().SetWalkAnimation(movementInput.magnitude > 0);
    }
    
    // this function integrates acceleration
    protected virtual float calculateSpeed(Vector3 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentVelocity += MovementData.acceleration * Time.deltaTime ;
        }
        else
        {
            currentVelocity -= MovementData.decceleration * Time.deltaTime;
        }
        // Returns velocity between 0 and maxSpeed
        //Vector3 v = Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }

    public void Knockback(float duration, float power, Vector3 direction)
    {
        Debug.Log("In knockback");
        knockback = true;
        // Vector2 direction = (bullet.direction).normalized;
        knockbackPower = power;
        knockbackTimer = duration;
        knockbackDirection = direction;
        //Debug.Log("Knockback direction: " + knockbackDirection);
        Vector3 k = -knockbackDirection * knockbackPower;
        //Debug.Log("k: " + k);
        rigidbody.AddForce(k.x, 0, k.z, ForceMode.Impulse);
        //knockback = false;
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
    }
}
