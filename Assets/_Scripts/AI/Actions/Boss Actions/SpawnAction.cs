using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : AIAction
{
    public EnemySpanwer spawner;
    void Start()
    {
        spawner = enemyBrain.transform.GetComponent<EnemySpanwer>();
    }
    public override void TakeAction()
    {
        if(SquidBoss.inSpawn)
        {
            Debug.Log("In Spawn");
            spawner.StartSpawn();
            SquidBoss.inSpawn = false; 
        }
    }
}
