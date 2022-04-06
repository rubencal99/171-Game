using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GraphUpdater : MonoBehaviour
{
    // public static GraphUpdateObject GUO = new GraphUpdateObject();
    public static bool InRoom;
    static Bounds b;
    void Start()
    {
        AstarPath graph = transform.GetComponent<AstarPath>();
        //NavGraph ObstacleGraph = graph.graphs[1];
        /*for(int i = 0; i < graph.graphs.Length; i++)
        {
            Debug.Log("Graph " + i + ": " + graph.graphs[i]);
        }*/
        
        //InvokeRepeating("GraphUpdate", 1f, .1f);
        //GraphUpdateWhole();
        //Invoke("GraphUpdateWhole", 0.5f);
    }
    
    void GraphUpdate()
    {
        if(InRoom)
        {
            AstarPath.active.UpdateGraphs(b);
        }
    }

    public static void SetNewBounds(Bounds bounds)
    {
        InRoom = true;
        b = bounds;
        // GUO = new GraphUpdateObject(bounds);
        // GUO.updatePhysics = true;
    }

    void GraphUpdateWhole()
    {
        Debug.Log("Active Graphs: " + AstarPath.active);
        AstarPath.active.Scan();
    }
}
