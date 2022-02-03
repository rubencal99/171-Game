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

    // MovementData is a scriptable object that contains data regarding movement
    [field: SerializeField]
    public MovementDataSO MovementData { get; set; }

    // This is only serialized for debugging purposes
    [SerializeField]
    protected float currentVelocity = 0;
    protected Vector2 movementDirection;

    // Modifyable field for Dodge
    // **************
    [SerializeField]
    protected float dodgeVelocity = 2;


    // This passes currentVelocity to AgentAnimations.AnimatePlayer
    // Hence SerializeField
    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

    private GameObject obj; 

    private void Start()
    {
        obj = GameObject.FindWithTag("Player");
        Debug.Log(obj);
    }

    private void Awake()
    {
        // Grabs RigidBody that the script is attached to
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        currentVelocity = calculateSpeed(movementInput);
    }
    
    // this function integrates acceleration
    private float calculateSpeed(Vector2 movementInput)
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
        if (obj.GetComponent<AgentInput>().dodging) {
            return Mathf.Clamp((currentVelocity * dodgeVelocity), 0, MovementData.maxDodgeSpeed);
        }
        // Returns velocity between 0 and maxSpeed
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }

    private void FixedUpdate()
    {   
        OnVelocityChange?.Invoke(currentVelocity);
        rigidbody2D.velocity = currentVelocity * movementDirection.normalized;
    }

    //********** Dodge function
    // Should player be able to dodge when not moving??
    public void dodge(Vector2 dodgeDirection) {
        Debug.Log(dodgeDirection);
        rigidbody2D.velocity = Vector2.zero; // set speed to zero
        rigidbody2D.velocity += dodgeDirection.normalized * dodgeVelocity; // create dodge
    }

}
