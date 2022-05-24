using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPresidentAction : AIAction
{
    public LayerMask layerMask;
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
            Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, layerMask);
            foreach(Collider collider in colliders)
            {
                //Debug.Log(collider.gameObject);
                allies.Add(collider.gameObject);
            }
        }

        int priority = -1;
        GameObject President = null;
        foreach(GameObject ally in allies)
        {
            if(ally.GetInstanceID() != enemyBrain.gameObject.GetInstanceID() && 
                ally.GetComponent<Enemy>().EnemyData.Priority > priority &&
                !ally.GetComponent<Enemy>().hasProtector)
            {
                if(President != null)
                {
                    President.GetComponent<Enemy>().hasProtector = false;
                }
                President = ally;
                priority = ally.GetComponent<Enemy>().EnemyData.Priority;
                ally.GetComponent<Enemy>().hasProtector = true;
                //Debug.Log("Potential President: " + President);
            }
        }
        // Sets Enemy w highest priority to assist
        if(President != null)
        {
            //Debug.Log("New President: " + President);
            enemyBrain.Target = President;
            aiMovementData.PointOfInterest = President.transform.position;
        }   
        
    }

    public bool InRoom()
    {
        return transform.root.GetComponent<RoomNode>();
    }
}
