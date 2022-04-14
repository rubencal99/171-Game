using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectPresidentAction : AIAction
{
    public LayerMask layerMask;
    public int DistanceFromPresident;
    Vector3 shieldPos;
    public override void TakeAction(){
        // Use LOS of President to Player
        Vector3 point = FindPoint();
        aiMovementData.PointOfInterest = point;
    }

    public Vector3 FindPoint()
    {
        Vector3 presidentPos = enemyBrain.Target.transform.position;
        Vector3 playerPos = Player.instance.transform.position;

        var direction = (playerPos - presidentPos).normalized;

        Debug.DrawRay(presidentPos, direction);
        //Debug.Log("President POS: " + presidentPos);
        //Debug.Log("Player POS: " + playerPos);

        shieldPos = presidentPos + (direction * DistanceFromPresident);
        shieldPos.y = 1;
        //RaycastHit2D hit = Physics2D.Raycast(presidentPos, direction, 100, layerMask);

        return shieldPos;


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(shieldPos, 0.5f);
    }
}