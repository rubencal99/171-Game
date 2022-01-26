using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StarChaseAction : AIAction
{
    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    public GameObject target;

    public float nextWaypointDistance = 3;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = transform.root.GetComponent<Seeker>();
        rb = transform.root.GetComponent<Rigidbody2D>();
        target = enemyBrain.Target;

        aiMovementData.PointOfInterest = (Vector2)target.transform.position;

        // This calls our UpdatePath func on 0.5 sec loop
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Update() {
        aiMovementData.PointOfInterest = (Vector2)target.transform.position;
        // Debug.Log("Point of Interest: " + aiMovementData.PointOfInterest);
    }    

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public override void TakeAction()
    {
        // Debug.Log("In StarChase");
        if (path == null)
        {
            // Debug.Log("path = null");
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            // Debug.Log("currentWaypoint >= path.vectorPath.Count");
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        // try transform.position instead of Parent<Transform>().position
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Debug.Log(direction);
        aiMovementData.Direction = direction;
        // aiMovementData.PointOfInterest = path.vectorPath[currentWaypoint];
        // Debug.Log("Direction: " + aiMovementData.Direction);
        Debug.Log("About to Move to " + aiMovementData.Direction);
        enemyBrain.Move(aiMovementData.Direction);
        Debug.Log("About to Aim at " + aiMovementData.PointOfInterest);
        enemyBrain.Aim(aiMovementData.PointOfInterest);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);


        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
