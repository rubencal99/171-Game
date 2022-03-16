using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDeadDecision : AIDecision
{
    public override bool MakeADecision()
    {
        if (enemyBrain.Target == null || enemyBrain.Target.GetComponent<Enemy>().isDying == true)
        {
            return true;
        }
        return false;
    }

}
