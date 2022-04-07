using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
 
    [System.Serializable]
    public class enemiesToSpawn
    {
        public GameObject Enemy;
        public int SpawnCount = 1;
    }
    public List<enemiesToSpawn> Enemies = new List<enemiesToSpawn>();
    public bool infiniteSpawn = false;
    public bool stopSpawn = false;
    public float spawnTime = 0.1f;
    public float spawnDelay = 0.2f;
   // public int numToSpawn = 1;

    // public int enemyDensity = 20;


    public float offset = 1.0f;

    public bool spawned = false;

     public int enemyCount = 0;

    private RoomNode thisroom;

    private string SpawnType;

    private bool canSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        thisroom = transform.parent.gameObject.GetComponent<RoomNode>();
        foreach(var order in Enemies) {
           for(int i = 0; i < order.SpawnCount; i++) {
               enemyCount++;
           }
       }
        // Debug.Log("initial Enemy count = " + enemyCount);
       // Enemies = Resources.LoadAll<GameObject>("_Prefabs/Enemies");


        // if(infiniteSpawn)
        //     InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        // else {
        //     for(int i = 0; i < Random.Range(numToSpawn/2  + 1, numToSpawn + (numToSpawn/4) + 1); i++) {
                 StartSpawn();
        //     }
        // }
    }

    // void Update() {
    //     if(spawned)
    //         oneStepCloser();
    // }

    public void StartSpawn()
    {
        StartCoroutine(invoke_spawn());
    }

    public IEnumerator invoke_spawn() {
        // canSpawn = false;
        // yield return new WaitForSeconds(spawnTime);
        // Invoke("SpawnObject", spawnTime);
        // canSpawn = true;
       foreach(var order in Enemies) {
           for(int i = 0; i < order.SpawnCount; i++) {
                StartCoroutine(SpawnObject(order.Enemy));
               Debug.Log("enemy spawned");
               yield return new WaitForSeconds(spawnDelay);
           }
       }
        // for(int i = 0; i < Random.Range(); i++) {
        //     Invoke("SpawnObject", spawnTime);
        //     yield return new WaitForSeconds(spawnTime);
        // }
    }
    public IEnumerator SpawnObject(GameObject source){
        // Vector3 offsetPosition = transform.position;
        // offsetPosition.x += Random.Range(-offset, offset);
        // offsetPosition.y += Random.Range(-offset, offset); 
        yield return new WaitForSeconds(spawnTime);
        RoomNode room = this.transform.parent.GetComponent<RoomNode>();
        //Vector3 room_center = new Vector3((float)room.roomCenter.x, (float)room.roomCenter.y, 0f);

        TileNode spawnTile = room.GrabValidTile();

        Vector3 offsetPosition = new Vector3(spawnTile.x, 2f, spawnTile.y);

        //var source = Enemies[Random.Range(0, Enemies.Length)];
        var clone = Instantiate(source, offsetPosition, Quaternion.identity);
        clone.name = source.name;
        clone.transform.parent = this.gameObject.transform.parent.transform;
        StartCoroutine(enable_brain(clone));
      //  clone.GetComponent<EnemyBrain>().enabled = true;

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

   public IEnumerator enable_brain(GameObject enemy) {
      enemy.transform.Find("WeaponParent").gameObject.SetActive(false);  
      yield return new WaitForSeconds(spawnDelay);
      enemy.GetComponent<EnemyBrain>().enabled = true;
      enemy.transform.Find("WeaponParent").gameObject.SetActive(true);
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
