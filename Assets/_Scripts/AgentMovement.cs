using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Script requires RigidBody attached to Player object
[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody2D;

    protected PlayerPassives Passives;

    // MovementData is a scriptable object that contains data regarding movement
    [field: SerializeField]
    public MovementDataSO MovementData { get; set; }

    // This is only serialized for debugging purposes
    [SerializeField]
    public float currentVelocity = 0;
    [SerializeField]
    protected Vector2 movementDirection;

    // This passes currentVelocity to AgentAnimations.AnimatePlayer
    // Hence SerializeField
    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }


    protected void Awake()
    {
        // Grabs RigidBody that the script is attached to
        rigidbody2D = GetComponent<Rigidbody2D>();

        // Grabs PlayerPassives script
        Passives = GetComponent<PlayerPassives>();

        // Resets MovementData from previous plays
        // MovementData.maxSpeed = DefaultSpeed;
    }

    // Takes Vector2 from AgentInput OnMovementKeyPressed
    public void MoveAgent(Vector2 movementInput)
    {
        // rigidbody2D.velocity = movementInput.normalized * currentVelocity;
        if (movementInput.magnitude > 0)
        {
            /* // Use this if we want car-like deceleration
            if (Vector2.Dot(movementInput.normalized, movementDirection) < 0)
                currentVelocity = 0;
            */
            movementDirection = movementInput.normalized;
        }
        currentVelocity = calculateSpeed(movementInput) * Passives.SpeedMultiplier;
        this.GetComponentInChildren<AgentAnimations>().SetWalkAnimation(movementInput.magnitude > 0);
    }
    
    // this function integrates acceleration
    protected virtual float calculateSpeed(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            currentVelocity += MovementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= MovementData.decceleration * Time.deltaTime;
        }
        // Returns velocity between 0 and maxSpeed
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }

    protected void FixedUpdate()
    {   
        OnVelocityChange?.Invoke(currentVelocity);
        rigidbody2D.velocity = currentVelocity * movementDirection.normalized;
    }
}
