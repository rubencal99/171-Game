using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public GameObject Enemy;
    public bool stopSpawn = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject(){
        var clone = Instantiate(Enemy, transform.position, Quaternion.identity);
        if(stopSpawn){
            CancelInvoke("SpawnObject");
        }
    }
}
