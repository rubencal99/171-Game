using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatchetMovement : BossMovement
{
    // this function integrates acceleration
    protected override float calculateSpeed(Vector3 movementInput)
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
    
    protected override void FixedUpdate()
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
        if(RatchetBoss.inAir && !RatchetBoss.inJump)
          {
            rigidbody.velocity += Vector3.down * Time.fixedDeltaTime / 100;
            return;
          }
        if(rigidbody != null && !knockback)
         rigidbody.velocity = currentVelocity * movementDirection.normalized;
    }
}
