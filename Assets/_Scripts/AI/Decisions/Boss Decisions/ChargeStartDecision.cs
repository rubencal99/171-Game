using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeStartDecision : AIDecision
{
  public override bool MakeADecision()
  {
    return RatchetBoss.inChargeStart;
  }
}
