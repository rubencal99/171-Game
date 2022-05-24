using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRightAction : AIAction
{
    public override void TakeAction()
    {
        //Debug.Log("In aim right: " + Vector3.right);
        Debug.Log("In aim right transform: " + enemyBrain.transform.position);
        Debug.Log("In aim right poi: " + aiMovementData.PointOfInterest);
        aiMovementData.PointOfInterest = enemyBrain.Weapon.transform.position + Vector3.right;
        
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
