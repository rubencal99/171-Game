using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneDestinationDecision : AIDecision
{
    public override bool MakeADecision()
    {
        if(SquidBoss.atCycloneDest)
        {
            
        }
        return SquidBoss.atCycloneDest;
    }


}
