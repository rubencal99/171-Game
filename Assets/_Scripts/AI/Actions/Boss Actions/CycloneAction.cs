using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneAction : AIAction
{
    public static bool tracker = true;
    public bool Tracker;
    public override void TakeAction()
    {
        /*if(SquidBoss.atCycloneDest == true)
        {
            SquidBoss.atCycloneDest = false;
        }*/
        Tracker = tracker;
        // Debug.Log("Distance from player: " + Vector2.Distance((Vector2)transform.position, aiMovementData.PointOfInterest));
        if(SquidBoss.inCyclone && tracker)
        {
            Debug.Log("In Cyclone");
            aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
            enemyBrain.Aim(aiMovementData.PointOfInterest);
            tracker = false;
            //SquidBoss.inCyclone = true;
            SquidBoss.cycloneAttempts--;
        }
        else if(Vector2.Distance((Vector2)transform.position, aiMovementData.PointOfInterest) <= 0.5 && SquidBoss.atCycloneDest == false)
        {
            Debug.Log("Reached Cyclone destination");
            aiMovementData.PointOfInterest = transform.position;
            enemyBrain.Aim(aiMovementData.PointOfInterest);
            // SquidBoss.inCyclone = true;
            SquidBoss.atCycloneDest = true;
            tracker = true;
            SquidBoss.cycloneTimer = SquidBoss.CycloneDuration;
            SquidBoss.cycloneAttempts--;
        }
        if(SquidBoss.cycloneAttempts <= 0)
        {
            SquidBoss.inCyclone = false;
            tracker = true;
        }
        Tracker = tracker;
    }

    public IEnumerator FindNewDestination()
    {
        SquidBoss.atCycloneDest = true;
        aiMovementData.PointOfInterest = transform.position;
        //enemyBrain.Aim(aiMovementData.PointOfInterest);
        yield return new WaitForSeconds(3f);
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Aim(aiMovementData.PointOfInterest);
        
        SquidBoss.inCyclone = true;
        SquidBoss.atCycloneDest = false;
        
        SquidBoss.cycloneAttempts--;
    }
}
