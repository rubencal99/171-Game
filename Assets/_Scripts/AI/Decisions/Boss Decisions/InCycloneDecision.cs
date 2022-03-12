using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCycloneDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return SquidBoss.inCyclone;
    }
}
