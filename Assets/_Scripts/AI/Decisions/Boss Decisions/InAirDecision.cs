using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirDecision : AIDecision
{
  public override bool MakeADecision()
  {
    return RatchetBoss.inAir;
  }
}
