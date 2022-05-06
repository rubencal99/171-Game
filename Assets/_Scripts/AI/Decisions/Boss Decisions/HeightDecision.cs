using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightDecision : AIDecision
{
    public YJumpAction jumpAction;

    void Start()
    {
        jumpAction = transform.parent.GetComponent<YJumpAction>();
    }
    public override bool MakeADecision()
    {
        //Debug.Log("Enemy Brain: " + enemyBrain);
        //Debug.Log("Jump Action: " + jumpAction);
        return enemyBrain.transform.position.y >= jumpAction.height;
    }
}