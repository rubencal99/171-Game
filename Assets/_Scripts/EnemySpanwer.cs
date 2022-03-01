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

     public int enemyDensity = 20;

    public float offset = 1.0f;

    public bool spawned = false;

    private RoomNode thisroom;

    // Start is called before the first frame update
    void Start()
    {
        thisroom = transform.parent.gameObject.GetComponent<RoomNode>();
        enemyCount = thisroom.area / enemyDensity;

        // Debug.Log("initial Enemy count = " + enemyCount);
       // Enemies = Resources.LoadAll<GameObject>("_Prefabs/Enemies");


        if(infiniteSpawn)
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        else {
            for(int i = 0; i < Random.Range(numToSpawn/2  + 1, numToSpawn + (numToSpawn/4) + 1); i++) {
                Invoke("SpawnObject", spawnTime);
            }
        }
    }

    // void Update() {
    //     if(spawned)
    //         oneStepCloser();
    // }

    public void SpawnObject(){
        // Vector3 offsetPosition = transform.position;
        // offsetPosition.x += Random.Range(-offset, offset);
        // offsetPosition.y += Random.Range(-offset, offset); 
        RoomNode room = this.transform.parent.GetComponent<RoomNode>();
        Vector3 room_center = new Vector3((float)room.roomCenter.x, (float)room.roomCenter.y, 0f);

        Vector3 offsetPosition = new Vector3(Random.Range(room_center.x - (room.width /2f) +2f, (room_center.x + (room.width /2f) - 2f)), 
                                        Random.Range(room_center.y - (room.length/2f) +2f, (room_center.y + (room.length /2f)-2f)),
                                            0f);

        var source = Enemies[Random.Range(0, Enemies.Length)];
        var clone = Instantiate(source, offsetPosition, Quaternion.identity);
        clone.name = source.name;
        clone.transform.parent = this.gameObject.transform.parent.transform;
        clone.GetComponent<EnemyBrain>().enabled = true;
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
             // Debug.Log("current enemy count = " + enemyCount); 
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
