using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneAction : AIAction
{
    public override void TakeAction()
    {
        if(!SquidBoss.inCyclone)
        {
            Debug.Log("In Cyclone");
            aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
            enemyBrain.Aim(aiMovementData.PointOfInterest);
            SquidBoss.inCyclone = true;
        } 
    }
}
