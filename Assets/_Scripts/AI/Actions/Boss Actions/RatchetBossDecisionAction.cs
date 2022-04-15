using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatchetBossDecisionAction : AIAction
{
  public override void TakeAction()
  {
      //SquidBoss.inDecision = true;
      //RatchetBoss.inDecision = true;
      int r = Random.Range(0, 90);
      Debug.Log("In Ratchet Boss decision action");
      Debug.Log("Random number = " + r);
      RatchetBoss.inCharge = true;
      Debug.Log("In Charge = " + RatchetBoss.inCharge);
      /*
      
      RatchetBoss.inJump = true;
      Debug.Log("In Jump = " + RatchetBoss.inJump);

      */
      
  }
}
