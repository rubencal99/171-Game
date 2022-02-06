using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsureDeadEnemyTargetAction : AIAction
{
    public override void TakeAction()
    {
        StarChaseAction chase = GetComponent<StarChaseAction>();
        chase.target = enemyBrain.Target;
    }
}
