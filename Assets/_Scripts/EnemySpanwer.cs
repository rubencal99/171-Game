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
    public int enemyCount;

    public float offset = 1.0f;

    public bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = this.numToSpawn;
        Debug.Log("initial Enemy count = " + enemyCount);
       // Enemies = Resources.LoadAll<GameObject>("_Prefabs/Enemies");


        if(infiniteSpawn)
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        else {
            for(int i = 0; i < Random.Range(numToSpawn/2, numToSpawn + (numToSpawn/4)); i++) {
                Invoke("SpawnObject", spawnTime);
            }
        }
    }

    // void Update() {
    //     if(spawned)
    //         oneStepCloser();
    // }

    public void SpawnObject(){
        Vector3 offsetPosition = transform.position;
        offsetPosition.x += Random.Range(-offset, offset);
        offsetPosition.y += Random.Range(-offset, offset);
        var clone = Instantiate(Enemies[Random.Range(0, Enemies.Length)], offsetPosition, Quaternion.identity);
        clone.transform.parent = this.gameObject.transform.parent.transform;
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
        // if(enemyCount <= 0)
        //     checkIfClear();
       
   }
//     public void checkIfClear() {
//         foreach(Transform child in transform.parent)
//         {
//         if(child.tag == "Spawner" || child.tag == "Door")
//             Destroy(child.gameObject);
//         }

//         Debug.Log("room cleared");
 
//     }
    
 }
