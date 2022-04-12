using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeRunAction : AIAction
{
  public LayerMask layerMask;
  RaycastHit hit;
  public override void TakeAction()
  {
      if(!RatchetBoss.hasDirection)
      {
          var d = enemyBrain.Target.transform.position - transform.position;
          RatchetBoss.chargeDirection = new Vector3(d.x, 0, d.z);
          RatchetBoss.hasDirection = true;

          Debug.DrawRay(transform.position, RatchetBoss.chargeDirection);
          hit = new RaycastHit();
          Physics.Raycast(transform.position, RatchetBoss.chargeDirection, out hit, 100, layerMask);
          aiMovementData.PointOfInterest = hit.transform.position;
          aiMovementData.PointerPosition = hit.transform.position;
      }
      aiMovementData.Direction = RatchetBoss.chargeDirection.normalized;
      // aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
      enemyBrain.Move(aiMovementData.Direction);
      //Debug.Log("Ratchet Charge Direction: " + RatchetBoss.chargeDirection);
  }

  public void OnDrawGizmos()
  {
    Gizmos.DrawSphere(hit.transform.position, 1);
  }
}
