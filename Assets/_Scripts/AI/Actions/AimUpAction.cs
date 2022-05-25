using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimUpAction : AIAction
{
    public override void TakeAction()
    {
        aiMovementData.PointOfInterest = enemyBrain.Weapon.transform.position + Vector3.forward;
        //Debug.Log("In aim up transform: " + enemyBrain.transform.position);
        //Debug.Log("In aim up poi: " + aiMovementData.PointOfInterest);
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
