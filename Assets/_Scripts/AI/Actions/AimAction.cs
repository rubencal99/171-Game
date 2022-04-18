using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAction : AIAction
{
    public override void TakeAction()
    {
        if(enemyBrain.Target == null)
        {
            return;
        }
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
