using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatchetBossDecisionAction : AIAction
{
  public override void TakeAction()
  {
      //SquidBoss.inDecision = true;
      //RatchetBoss.inDecision = true;
      int r = Random.Range(60, 100);
      Debug.Log("In Ratchet Boss decision action");
      Debug.Log("Random number = " + r);
      if(r < 30)
      {
        RatchetBoss.inChargeStart = true;
        RatchetBoss.inCharge = true;
        Debug.Log("In Charge = " + RatchetBoss.inChargeStart);
      }
      else if(r < 60)
      {
        RatchetBoss.inSlam = true;
        Debug.Log("In Slam = " + RatchetBoss.inSlam);
      }
      else
      {
        RatchetBoss.inJump = true;
         Debug.Log("In Jump = " + RatchetBoss.inJump);
      }
      
  }
}
