using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDeadDecision : AIDecision
{
    public override bool MakeADecision()
    {
        if (enemyBrain.Target == null || enemyBrain.Target == Player.instance.gameObject)
        {
            return true;
        }   
        return false;
    }

}
