using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSlamDecision : AIDecision
{
  public override bool MakeADecision()
  {
    return RatchetBoss.inSlam;
  }
}
