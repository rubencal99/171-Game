using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearChaseAction : AIAction
{
    public override void TakeAction()
    {
        var direction = (Vector3)aiMovementData.PointOfInterest - transform.position;
        aiMovementData.Direction = direction.normalized;
        // aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Move(aiMovementData.Direction);
        // enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
