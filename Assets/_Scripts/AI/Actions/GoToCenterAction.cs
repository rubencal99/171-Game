using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCenterAction : AIAction
{
    public Vector2 center;
    public RoomNode room;

    void Start()
    {
        room = enemyBrain.transform.parent.GetComponent<RoomNode>();
    }

    public override void TakeAction()
    {
        Debug.Log("In Go to Center");
        if(room)
        {
            center = (Vector2)room.roomCenter;
        }
        Vector2 cpos = new Vector2(enemyBrain.transform.position.x, enemyBrain.transform.position.z);

        var direction = center - cpos;
        aiMovementData.Direction = direction.normalized;
        aiMovementData.PointOfInterest = new Vector3(center.x, enemyBrain.transform.position.y, center.y);
        enemyBrain.Move(aiMovementData.Direction);
    }
}
