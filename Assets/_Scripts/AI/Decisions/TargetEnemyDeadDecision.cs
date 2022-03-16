using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemyDeadDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return enemyBrain.Target == null;
    }

}
