using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectPresidentAction : AIAction
{
    public LayerMask layerMask;
    public int DistanceFromPresident;
    Vector2 shieldPos;
    public override void TakeAction(){
        // Use LOS of President to Player
        Vector2 point = FindPoint();
        aiMovementData.PointOfInterest = point;
    }

    public Vector2 FindPoint()
    {
        Vector2 presidentPos = enemyBrain.Target.transform.position;
        Vector2 playerPos = Player.instance.transform.position;

        var direction = (playerPos - presidentPos).normalized;

        Debug.DrawRay(presidentPos, direction);
        Debug.Log("President POS: " + presidentPos);
        Debug.Log("Player POS: " + playerPos);

        shieldPos = presidentPos + (direction * DistanceFromPresident);

        //RaycastHit2D hit = Physics2D.Raycast(presidentPos, direction, 100, layerMask);

        return shieldPos;


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(shieldPos, 0.5f);
    }
}