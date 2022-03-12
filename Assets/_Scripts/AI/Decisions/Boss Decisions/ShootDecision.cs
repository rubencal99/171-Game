using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDecision : AIDecision
{
    public override bool MakeADecision()
    {
        if(SquidBoss.inShoot)
        {
            // Debug.Log("Transitioning into Shoot");
        }
        else
        {
            // Debug.Log("Transitioning out of Shoot");
            enemyBrain.StopAttack();
        }
        return SquidBoss.inShoot;
    }
}
