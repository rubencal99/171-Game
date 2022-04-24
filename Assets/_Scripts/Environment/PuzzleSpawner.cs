using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpawner : EnemySpanwer
{
    public override void Update() {
        if(Waves.Count > 0) {
            if(curEnemies <= 0)
                Waves[0].waveDelay -= Time.deltaTime;
                          

            if(Waves[0].waveDelay <= 0.0f) {
                StartSpawn(Waves[0].Enemies);
                //Waves[0] = Waves[0];
                Waves.Remove(Waves[0]);
                checkTimer = spawnTime + spawnDelay + 0.1f;
            }
        }
    }


    
 }
