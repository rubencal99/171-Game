using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// This allows enemy to patrol area randomly based on radius
public class PatrolAction : StarChaseAction
{
    public float radius;
    private float pointDistance = 0.01f;

    public float waitTime = 3f;
    private float currentWait;

    public override void Start()
    {
        seeker = transform.parent.parent.GetComponent<Seeker>();
        rb = transform.parent.parent.GetComponent<Rigidbody>();
        target = enemyBrain.Target;
        currentWait = waitTime;

        ChoosePoint();
    }

    public override void Update(){
        currentWait -= Time.deltaTime;
    }

    // This function randomly chooses point in radius for patrol
    protected void ChoosePoint() 
    {
        float x = UnityEngine.Random.Range(-radius, radius);
        float z = UnityEngine.Random.Range(-radius, radius);
        aiMovementData.PointOfInterest = new Vector3(rb.position.x + x, 0, rb.position.z + z);

        Vector3 direction = (aiMovementData.PointOfInterest - rb.position).normalized;
        aiMovementData.Direction = direction;
        // Debug.Log("Patrol point: (" + aiMovementData.PointOfInterest.x + ", " + aiMovementData.PointOfInterest.y + ")");
    }

    public override void TakeAction()
    {
        enemyBrain.StopAttack();
        if (currentWait <= 0)
        {
            ChoosePoint();
            enemyBrain.Move(aiMovementData.Direction);
            enemyBrain.Aim(aiMovementData.PointOfInterest);
            currentWait = waitTime;
        }
        var distance = Vector3.Distance(aiMovementData.PointOfInterest, rb.position);
        // Debug.Log("Distance = " + distance);
        if (distance < pointDistance){
            aiMovementData.Direction = Vector3.zero;
            enemyBrain.Move(aiMovementData.Direction);
            enemyBrain.Aim(aiMovementData.PointOfInterest);
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rb.position, aiMovementData.PointOfInterest);
        Gizmos.color = Color.white;
    }
}
