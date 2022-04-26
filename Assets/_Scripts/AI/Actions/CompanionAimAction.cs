using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionAimAction : AIAction
{
    public Companion companion;
    public override void TakeAction()
    {
        if(companion.enemyTarget == null)
        {
            return;
        }
        aiMovementData.PointOfInterest = companion.enemyTarget.transform.position;
        enemyBrain.Aim(aiMovementData.PointOfInterest);
    }
}
