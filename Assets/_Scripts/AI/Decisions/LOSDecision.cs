using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSDecision : AIDecision
{
    public LayerMask layerMask;
    GameObject lastHit;
    Vector3 collision = Vector3.zero;
    // LayerMask enemies = LayerMask.GetMask("Enemy");
    // LayerMask Colliders;
    public override bool MakeADecision()
    {
        // Colliders = ~enemies;
        var poi = aiMovementData.PointOfInterest;
        var pos = enemyBrain.Weapon.muzzle.transform.position;
        var direction = (enemyBrain.Target.transform.position - pos);
        var ray = new Ray(pos, direction);
        Debug.DrawRay(pos, direction);
        // Debug.Log("In LOS Decision");

        /*RaycastHit hit;// = Physics2D.Raycast(pos, direction, layerMask);
        //Debug.Log("Hit point = " + hit.transform.position);
        if (Physics.Raycast(pos, direction, out hit, 100, layerMask)){
            Debug.Log("Hit point = " + hit.transform.position);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")){
                Debug.Log("Hitting player");
                collision = hit.point;
                return true;
            }
        }
        return false;*/

        // Arbitrary distance, may be changed using EnemyData Range
        RaycastHit hit;
        Physics.Raycast(pos, direction, out hit, 100, layerMask);
        // Debug.Log("Hit point = " + hit.transform.position);
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
            // Debug.Log("LOS Hitting Obstacles");
            collision = hit.point;
            return false;
        }
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")){
            // Debug.Log("LOS Hitting Player");
            collision = hit.point;
            return true;
        }
        return false;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
