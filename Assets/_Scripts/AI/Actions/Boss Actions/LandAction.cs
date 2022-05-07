using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAction : AIAction
{
  public bool landed = true;
  public override void TakeAction()
  {
    Debug.Log("In land action");
    if(RatchetBoss.inAir)
    {
      RatchetBoss.inJump = false;
      landed = false;
    }
  }

  void OnTriggerEnter(Collider other)
  {
    Debug.Log("In Land Action trigger");
    // Checks if boss has collided with floor
    if(other.gameObject.layer == LayerMask.NameToLayer("Base Floor"))
    {
      Debug.Log("About to attack");
      enemyBrain.Weapon.ForceShoot();
      RatchetBoss.inAir = false;
      landed = true;
    }
  }

  void OnCollisionEnter(Collision other)
  {
    Debug.Log("In Land Action collision");
    // Checks if boss has collided with floor
    if(other.gameObject.layer == LayerMask.NameToLayer("Base Floor"))
    {
      Debug.Log("About to attack");
      enemyBrain.Weapon.ForceShoot();
      RatchetBoss.inAir = false;
      landed = true;
    }
  }
}
