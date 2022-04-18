using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryDecision : AIDecision
{
  public override bool MakeADecision()
  {
    return RatchetBoss.inRecovery;
  }
}
