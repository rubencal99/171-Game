using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : AIAction
{
    public override void TakeAction()
    {
        Debug.Log("In shoot");
        // This is basically the distance decision
        var distance = Vector3.Distance(enemyBrain.Target.transform.position, transform.position);
        if (Mathf.Abs(distance) < aiActionData.Range){
            enemyBrain.Attack();
        }
    }
}
