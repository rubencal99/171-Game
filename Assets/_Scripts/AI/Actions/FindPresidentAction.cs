using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPresidentAction : AIAction
{
    public override void TakeAction(){
        // If we don't have Target or if Target is still defauly Player
        /*if(enemyBrain.Target == null || enemyBrain.Target == Player.instance)
        {
            FindPresident();
        }*/
        FindPresident();
        //GameObject allyToBuff = FindAlly();
    }

    public void FindPresident()
    {
        // Find allies
        List<GameObject> allies;
        if(InRoom())
        {
            // If in a room, find ally in children of room
            allies = new List<GameObject>();
            foreach(Transform child in transform.root)
            {
                if(child.tag == "Enemy")
                {
                    allies.Add(child.gameObject);
                }
            }
        }
        else
        {
            // If in root of hierarchy, find all enemies in scene
            allies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        }

        int priority = 0;
        GameObject President = null;
        foreach(GameObject ally in allies)
        {
            if(ally.GetInstanceID() != enemyBrain.gameObject.GetInstanceID() && 
                ally.GetComponent<Enemy>().EnemyData.Priority > priority)
            {
                President = ally;
            }
        }
        // Sets Enemy w highest priority to assist
        enemyBrain.Target = President;
    }

    public bool InRoom()
    {
        return transform.root.GetComponent<RoomNode>();
    }
}
