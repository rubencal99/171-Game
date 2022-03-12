using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDecisionAction : AIAction
{
  public override void TakeAction()
  {
      //SquidBoss.inDecision = true;
      int r = Random.Range(0, 90);
      Debug.Log("In Boss decision action");
      Debug.Log("Random number = " + r);
      if (r <= 30)
      {
          SquidBoss.inCyclone = true;
      }
      else if (r <= 50)
      {
          Debug.Log("In Chase = true");
          SquidBoss.inChase = true;
      }
      else// if (r <= 90)
      {
          SquidBoss.inShoot = true;
      }
      /*else
      {
          SquidBoss.inSpawn = true;
      }*/
      SquidBoss.inDecision = false;
  }
}
