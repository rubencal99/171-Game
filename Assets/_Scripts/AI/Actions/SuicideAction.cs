using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAction : AIAction
{
  public override void TakeAction()
  {
    // Here is where we trigger an explosion animation + effects
    GetComponentInChildren<EnemyGrenade>().enabled = true;
    Debug.Log("In Suicide");
    Invoke("WaitToDestroy", 0.5f);
  }
  void WaitToDestroy()
  {
      Destroy(enemyBrain.gameObject);
  }
}
