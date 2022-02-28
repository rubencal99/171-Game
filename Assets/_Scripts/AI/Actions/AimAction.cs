using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAction : AIAction
{
    public override void TakeAction()
    {
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
