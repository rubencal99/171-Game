using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAction : AIAction
{
  public override void TakeAction(){
        var direction = enemyBrain.Target.transform.position - transform.position;
        aiMovementData.Direction = direction.normalized;
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Move(aiMovementData.Direction);
        enemyBrain.Aim(aiMovementData.PointOfInterest);
  }
}
