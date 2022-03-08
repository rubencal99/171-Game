using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTimeDecision : AIDecision
{
    public float Timer = 4f;
    public float time = 4f;
    public override bool MakeADecision()
    {
        time -= Time.deltaTime;
        if (time <= 0.1)
        {
            time = Timer;

            SquidBoss.atCycloneDest = false;
            aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
            enemyBrain.Aim(aiMovementData.PointOfInterest);

            return true;
        }
        return false;
    }


}
