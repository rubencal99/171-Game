using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeDecision : AIDecision
{
  public override bool MakeADecision()
  {
    return RatchetBoss.inCharge;
  }
}
