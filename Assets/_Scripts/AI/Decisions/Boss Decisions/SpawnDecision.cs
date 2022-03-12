using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return SquidBoss.inSpawn;
    }
}