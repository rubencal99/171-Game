using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YJumpAction : AIAction
{

    public float jumpForce;
    public float height;
    public float speed;
    public override void TakeAction()
    {
        //enemyBrain.enemy.agentMovement.rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        RatchetBoss.inAir = true;

        enemyBrain.transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
