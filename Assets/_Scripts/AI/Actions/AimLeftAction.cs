using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLeftAction : AIAction
{
    public override void TakeAction()
    {
        //Debug.Log("In aim left: " + Vector3.left);
        aiMovementData.PointOfInterest = enemyBrain.Weapon.transform.position + Vector3.left;
        //Debug.Log("In aim left transform: " + enemyBrain.transform.position);
        //Debug.Log("In aim left poi: " + aiMovementData.PointOfInterest);
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
