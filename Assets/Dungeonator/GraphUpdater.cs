using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GraphUpdater : MonoBehaviour
{
    public static GraphUpdateObject GUO = new GraphUpdateObject();
    public static bool InRoom;
    static Bounds b;
    void Start()
    {
        InvokeRepeating("GraphUpdate", 1f, .1f);
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
}
