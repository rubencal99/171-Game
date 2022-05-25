using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownAction : AIAction
{
    public override void TakeAction()
    {
        //Debug.Log("In aim down: " + Vector3.back);
        aiMovementData.PointOfInterest = enemyBrain.Weapon.transform.position + Vector3.back;
        //Debug.Log("In aim down transform: " + enemyBrain.transform.position);
        //Debug.Log("In aim down poi: " + aiMovementData.PointOfInterest);
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
