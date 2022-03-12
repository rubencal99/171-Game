using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !SquidBoss.inDecision;
    }
}

