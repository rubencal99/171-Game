using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCenterAction : AIAction
{
    public Vector2 center;
    public RoomNode room;
    public YJumpAction jumpAction;

    void Start()
    {
        if(enemyBrain.transform.parent)
        {
            room = enemyBrain.transform.parent.GetComponent<RoomNode>();
        }
    }

    public override void TakeAction()
    {
        Debug.Log("In Go to Center");
        if(room)
        {
            center = (Vector2)room.roomCenter;
        }
        Vector2 cpos = new Vector2(enemyBrain.transform.position.x, enemyBrain.transform.position.z);

        var direction = (center - cpos).normalized;
        aiMovementData.Direction = new Vector3(direction.x, 0, direction.y);
        aiMovementData.PointOfInterest = new Vector3(center.x, jumpAction.height, center.y);
        enemyBrain.Move(aiMovementData.Direction);
    }

    /*void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(center.x, jumpAction.height, center.y), 1);
    }*/
}
