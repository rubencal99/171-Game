using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayerAction : AIAction
{
    public override void TakeAction()
    {
        //aiMovementData.PointOfInterest = Player.instance.transform.position;
        enemyBrain.Aim(Player.instance.transform.position);
    }
}
