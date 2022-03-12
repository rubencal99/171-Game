using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class AIPathfinding : MonoBehaviour
{
    public EnemyBrain brain;
    protected AIActionData aiActionData;
    protected AIMovementData aiMovementData;

    public GameObject target;

    public float speed;
    public float nextWaypointDistance = 3;

    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        aiActionData = GetComponentInChildren<AIActionData>();
        aiMovementData = GetComponentInChildren<AIMovementData>();
        brain = GetComponent<EnemyBrain>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = brain.Target;

        // This calls our UpdatePath func on 0.5 sec loop
        InvokeRepeating("UpdatePath", 0f, 0.5f);
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
        if (p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
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

        // try transform.position instead of Parent<Transform>().position
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Debug.Log(direction);
        aiMovementData.Direction = direction;
        aiMovementData.PointOfInterest = path.vectorPath[currentWaypoint];
        brain.Move(aiMovementData.Direction);
        brain.Aim(aiMovementData.PointOfInterest);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);


        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
