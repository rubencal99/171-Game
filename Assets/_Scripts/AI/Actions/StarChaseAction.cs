using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class StarChaseAction : AIAction
{
    protected Path path;
    protected int currentWaypoint = 0;
    protected bool reachedEnd = false;

    public GameObject target;

    public float nextWaypointDistance = 0.5f;

    protected Seeker seeker;
    protected Rigidbody2D rb;
    protected CapsuleCollider2D movementCollider;

    // Start is called before the first frame update
    public virtual void Start()
    {
        seeker = transform.parent.parent.GetComponentInChildren<Seeker>();
        rb = transform.parent.parent.GetComponent<Rigidbody2D>();
        movementCollider = seeker.transform.GetComponent<CapsuleCollider2D>();
        target = enemyBrain.Target;

        aiMovementData.PointOfInterest = (Vector2)target.transform.position;

        // This calls our UpdatePath func on 0.5 sec loop
        InvokeRepeating("UpdatePath", 0f, .1f);
    }

    public virtual void Update() {
        if(target)
            aiMovementData.PointOfInterest = (Vector2)target.transform.position;
        // Debug.Log("Point of Interest: " + aiMovementData.PointOfInterest);
    }    

    protected void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(movementCollider.transform.position, target.transform.position, OnPathComplete);
        }
    }

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public override void TakeAction()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)movementCollider.transform.position).normalized;
        aiMovementData.Direction = direction;
        // aiMovementData.PointOfInterest = path.vectorPath[currentWaypoint];

        enemyBrain.Move(aiMovementData.Direction);
        // enemyBrain.Aim(aiMovementData.PointOfInterest);

        float distance = Vector2.Distance(movementCollider.transform.position, path.vectorPath[currentWaypoint]);


        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
