using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossResetAction : AIAction
{
  public _BaseBoss boss;

  public override void TakeAction()
  {
    boss.Reset();
  }
}
