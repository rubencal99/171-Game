using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : AgentMovement
{
    // Takes Vector2 from AgentInput OnMovementKeyPressed
    public override void MoveAgent(Vector2 movementInput)
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
}
