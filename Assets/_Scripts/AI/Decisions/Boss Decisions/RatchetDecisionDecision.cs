using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatchetDecisionDecision : AIDecision
{
    public override bool MakeADecision()
    {
        //Debug.Log("In Decision: " + RatchetBoss.inDecision);
        return !RatchetBoss.inDecision;
    }
}
