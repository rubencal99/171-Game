 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSDecision : AIDecision
{
    public LayerMask layerMask;
    GameObject lastHit;
    Vector2 collision = Vector2.zero;
    // LayerMask enemies = LayerMask.GetMask("Enemy");
    // LayerMask Colliders;
    public override bool MakeADecision()
    {
        // Colliders = ~enemies;
        var poi = aiMovementData.PointOfInterest;
        var pos = this.transform.position;
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

        // Arbitrary distance, may be changed usiong EnemyData Range
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, 100, layerMask);
        // Debug.Log("Hit point = " + hit.transform.position);
        Debug.Log("Rayhit Name: " + hit.transform.name.ToString() + "  Rayhit Layer: " + hit.transform.gameObject.layer.ToString());
        Debug.Log("Layer Name: " + LayerMask.LayerToName(hit.transform.gameObject.layer));
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")){
            Debug.Log("Hitting player");
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
