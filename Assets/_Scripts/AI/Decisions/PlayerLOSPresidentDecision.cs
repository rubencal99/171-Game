using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLOSPresidentDecision : AIDecision
{
    public LayerMask layerMask;
    GameObject lastHit;
    Vector3 collision = Vector3.zero;
    // LayerMask enemies = LayerMask.GetMask("Enemy");
    // LayerMask Colliders;
    public override bool MakeADecision()
    {
        // Colliders = ~enemies;
        var playerPos = Player.instance.transform.position;
        var presidentPos = enemyBrain.Target.transform.position;
        var direction = (playerPos - presidentPos);
        var ray = new Ray(presidentPos, direction);
        Debug.DrawRay(presidentPos, direction);
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
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(presidentPos, direction, out hit, 100, layerMask);
        
        // Debug.Log("Hit point = " + hit.transform.position);
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
            // Debug.Log("LOS Hitting Obstacles");
            collision = hit.point;
            return false;
        }
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") && hit.transform.gameObject == enemyBrain.gameObject){
            Debug.Log("Protecting president");
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
}
