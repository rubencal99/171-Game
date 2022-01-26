using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    public GameObject[]  Enemies;
    public bool infiniteSpawn = false;
    public bool stopSpawn = false;
    public float spawnTime = 1.0f;
    public float spawnDelay = 1.0f;
    public int numToSpawn = 1;
    private int enemyCount;

    private bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = this.numToSpawn;
        Debug.Log("initial Enemy count = " + enemyCount);
       // Enemies = Resources.LoadAll<GameObject>("_Prefabs/Enemies");


        if(infiniteSpawn)
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        else {
            for(int i = 0; i < numToSpawn; i++) {
                Invoke("SpawnObject", spawnTime);
            }
        }
    }

    void Update() {
        if(spawned)
            oneStepCloser();
    }

    public void SpawnObject(){
        Vector3 offsetPosition = transform.position;
        offsetPosition.x += Random.Range(-1.5f, 1.5f);
        offsetPosition.y += Random.Range(-1.5f, 1.5f);
        var clone = Instantiate(Enemies[Random.Range(0, Enemies.Length)], offsetPosition, Quaternion.identity);
        clone.transform.parent = this.gameObject.transform;
        if(stopSpawn){
            CancelInvoke("SpawnObject");
        }

        spawned = true;

    }

     // Update is called once per frame
   public void oneStepCloser() {
      enemyCount = 0;
       foreach(Transform child in transform)
       { 
           if(child.tag == "Enemy") {
                enemyCount++;
           }
             Debug.Log("current enemy count = " + enemyCount); 
        }
        if(enemyCount <= 0)
            checkIfClear();
       
   }
    public void checkIfClear() {
        foreach(Transform child in transform.parent)
        {
        if(child.tag == "Spawner" || child.tag == "Door")
            Destroy(child.gameObject);
        }

        Debug.Log("room cleared");
 
    }
    
}
