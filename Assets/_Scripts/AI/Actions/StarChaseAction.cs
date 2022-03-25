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
    protected Rigidbody rb;
    protected CapsuleCollider movementCollider;

    // Start is called before the first frame update
    public virtual void Start()
    {
        seeker = transform.parent.parent.GetComponentInChildren<Seeker>();
        rb = transform.parent.parent.GetComponent<Rigidbody>();
        movementCollider = seeker.transform.GetComponent<CapsuleCollider>();
        target = enemyBrain.Target;

        aiMovementData.PointOfInterest = (Vector3)target.transform.position;

        // This calls our UpdatePath func on 0.5 sec loop
        InvokeRepeating("UpdatePath", 0f, .1f);
    }

    public virtual void Update() {
        target = enemyBrain.Target;
        if(target && enemyBrain.enemy.EnemyData.aimsAtTarget == true)
            aiMovementData.PointOfInterest = (Vector3)target.transform.position;
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

        Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint + 1] - (Vector3)movementCollider.transform.position).normalized;
       // Debug.Log("Path point: " + path.vectorPath[currentWaypoint + 1]);
       // Debug.Log("Movement collider position: " + movementCollider.transform.position);
        //direction.z = direction.y;
        //direction.y = 0;
      //  Debug.Log("Star Chase Direction: " + direction);
        aiMovementData.Direction = direction;
        // aiMovementData.PointOfInterest = path.vectorPath[currentWaypoint];

        enemyBrain.Move(aiMovementData.Direction);
        // enemyBrain.Aim(aiMovementData.PointOfInterest);

        float distance = Vector3.Distance(movementCollider.transform.position, path.vectorPath[currentWaypoint]);


        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
