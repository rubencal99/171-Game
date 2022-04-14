using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDecision : AIDecision
{
  public override bool MakeADecision()
  {
    return RatchetBoss.inJump;
  }
}
