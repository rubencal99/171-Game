using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAction : AIAction
{
  public float waitTime;
  public override void TakeAction()
  {
    // Here is where we trigger an explosion animation + effects
    GetComponentInChildren<EnemyGrenade>().enabled = true;
    Debug.Log("In Suicide");
    Invoke("WaitToDestroy", waitTime);
  }
  void WaitToDestroy()
  {
      Destroy(enemyBrain.gameObject);
  }
}
