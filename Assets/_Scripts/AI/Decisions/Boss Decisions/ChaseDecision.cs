using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseDecision : AIDecision
{
    public override bool MakeADecision()
    {
        if(SquidBoss.inChase)
        {
            // Debug.Log("Transitioning into Chase");
        }
        else
        {
            // Debug.Log("Transitioning out of Chase");
            enemyBrain.StopAttack();
        }
        return SquidBoss.inChase;
    }
}
