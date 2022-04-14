using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMovement : BossMovement
{
    /*public CapsuleCollider movementCollider;
    public void Awake()
    {

    }*/

    // Takes Vector2 from AgentInput OnMovementKeyPressed
    public override void MoveAgent(Vector3 movementInput)
    {
        // rigidbody2D.velocity = movementInput.normalized * currentVelocity;
        if (movementInput.magnitude > 0)
        {
            /// Use this if we want car-like deceleration
            if (SquidBoss.inCyclone && Vector2.Dot(movementInput.normalized, movementDirection) < 0)
            {
                currentVelocity = 0;
            }

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
        if(SquidBoss.inCyclone && !SquidBoss.atCycloneDest)
        {
            return Mathf.Clamp(currentVelocity, 0, MovementData.maxDodgeSpeed);
        }
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxRunSpeed);
    }
}
