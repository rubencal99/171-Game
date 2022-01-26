using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public GameObject Enemy;
    public bool infiniteSpawn = false;
    public bool stopSpawn = false;
    public float spawnTime = 1.0f;
    public float spawnDelay = 1.0f;
    public int numToSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(infiniteSpawn)
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        else {
            for(int i = 0; i < numToSpawn; i++) {
                Invoke("SpawnObject", spawnTime);
            }
        }
    }

    public void SpawnObject(){
        Vector3 offsetPosition = transform.position;
        offsetPosition.x += Random.Range(-1.5f, 1.5f);
        offsetPosition.y += Random.Range(-1.5f, 1.5f);
        offsetPosition.z += Random.Range(-1.5f, 1.5f);
        var clone = Instantiate(Enemy, offsetPosition, Quaternion.identity);
        if(stopSpawn){
            CancelInvoke("SpawnObject");
        }
    }
}
