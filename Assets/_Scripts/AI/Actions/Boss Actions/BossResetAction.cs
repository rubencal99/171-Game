using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossResetAction : AIAction
{
  public SquidBoss boss;

  public override void TakeAction()
  {
    boss.Reset();
  }
}
