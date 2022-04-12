using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YJumpAction : AIAction
{

    public float jumpForce;
    public override void TakeAction()
    {
        enemyBrain.enemy.agentMovement.rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
