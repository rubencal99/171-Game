using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceDecision : AIDecision
{
    [field: SerializeField]
    [field: Range(0.1f, 10)]
    public float Distance { get; set; } = 5;

    public override bool MakeADecision()
    {
        var d = Vector3.Distance(Player.instance.transform.position, transform.position);
        Debug.Log("Distance: " + d);
        //Debug.Log("Distance from Target: " + d);
        if (d < Distance)
        {
            aiActionData.TargetSpotted = true;
        }
        else
        {
            aiActionData.TargetSpotted = false;
        }
        return aiActionData.TargetSpotted;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Distance);
        Gizmos.color = Color.white;
    }
}
