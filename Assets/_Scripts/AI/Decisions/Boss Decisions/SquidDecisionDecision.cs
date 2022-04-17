using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidDecisionDecision : AIDecision
{
    public override bool MakeADecision()
    {
        Debug.Log("In Decision: " + SquidBoss.inDecision);
        return !SquidBoss.inDecision;
    }
}

