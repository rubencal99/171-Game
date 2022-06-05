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

     [System.Serializable]
    public class waves
    {
        public List<enemiesToSpawn> Enemies;

        public float waveDelay;

    }
   // public List<enemiesToSpawn> Enemies = new List<enemiesToSpawn>();

   public List<waves> Waves = new List<waves>();
    public bool infiniteSpawn = false;
    public bool stopSpawn = false;
    public float spawnTime = 0.1f;
    public float spawnDelay = 0.4f;
    public float offsetY = 1f;

    public LayerMask layerMask;
    public int numToSpawn = 10;

    public int enemyDensity = 20;


    public float offset = 1.0f;

    public bool spawned = false;

     public int enemyCount = 0;

    protected RoomNode thisroom;

    protected float checkTimer = 1.0f;

    protected string SpawnType;

    protected int curEnemies;

    protected bool canSpawn = true;

    protected waves curWave;

    public bool justEntered = false;

    public int density = 500;

    public int numEnemiesPerWave = 0;


    // Start is called before the first frame update
    protected void Start()
    {
        thisroom = transform.parent.gameObject.GetComponent<RoomNode>();
        numToSpawn = thisroom.validTileList.Count / enemyDensity;
        if(Waves.Count != 0)
            numEnemiesPerWave = (int)(numToSpawn / Waves.Count);
        
        Debug.Log("Room tile count: " + thisroom.validTileList.Count);
        Debug.Log("Total number of enemies to spawn: " + numToSpawn);
        Debug.Log("Number of enemies per wave: " + numEnemiesPerWave);
        foreach(var wave in Waves)
        {
            int waveCount = 0;
            foreach(var order in wave.Enemies) {
                for(int i = 0; i < order.SpawnCount; i++) {
                // wave.enemyCount++;
                    enemyCount++;
                    waveCount++;
                    if(wave == Waves[0])
                        curEnemies++;
                    if(waveCount >= numEnemiesPerWave)
                    {
                        Debug.Log("Enemy count for wave exceeded");
                        break;
                    }
                }
        }
            
        }
        curWave = Waves[0];

         //Debug.Log("initial Enemy count = " + Waves.Count);
       // Enemies = Resources.LoadAll<GameObject>("_Prefabs/Enemies");


        // if(infiniteSpawn)
        //     InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
        // else {
        //     for(int i = 0; i < Random.Range(numToSpawn/2  + 1, numToSpawn + (numToSpawn/4) + 1); i++) {
        
        //     }
        // }
    }

    public virtual void Update() {
        //Debug.Log("Wave count = " + Waves.Count + "curEnemies = " + curEnemies);
        if(Waves.Count > 0) {
            if(curEnemies <= 0)
                Waves[0].waveDelay -= Time.deltaTime;
                          

            if(Waves[0].waveDelay <= 0.0f) {
                if(Waves[0].Enemies.Count > 0)
                    StartSpawn(Waves[0].Enemies);
                //Waves[0] = Waves[0];
                Waves.Remove(Waves[0]);
                checkTimer = spawnTime + spawnDelay + 0.1f;
            }
        }
        else if(curEnemies <= 0 && checkTimer <= 0.0f)
            transform.parent.GetComponent<RoomClearCheck>().checkIfClear();

        curEnemies = oneStepCloser();
        checkTimer -= Time.deltaTime;
       
    }

    public void StartSpawn(List<enemiesToSpawn> e)
    {
        StartCoroutine(invoke_spawn(e));
    }

    public IEnumerator invoke_spawn(List<enemiesToSpawn> e) {
        // canSpawn = false;
        // yield return new WaitForSeconds(spawnTime);
        // Invoke("SpawnObject", spawnTime);
        // canSpawn = true;
        if(e != null)
            foreach(var order in e) {
                if(order != null && thisroom != null)
                    for(int i = 0; i < (order.SpawnCount * (thisroom.area / density)); i++) {
                        
                        if(order.Enemy != null)
                            StartCoroutine(SpawnObject(order.Enemy));
                    // Debug.Log("enemy spawned");
                        yield return new WaitForSeconds(spawnDelay);
                    }
            }
       
         spawned = true;
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

        Vector3 offsetPosition = new Vector3(spawnTile.x, offsetY, spawnTile.y);

        Collider[] colliders = Physics.OverlapSphere(offsetPosition, 1f, layerMask);

        int count = 0;
        while(colliders.Length > 0 && count < 5)
        {
            spawnTile = room.GrabValidTile();
            offsetPosition = new Vector3(spawnTile.x, offsetY, spawnTile.y);
            colliders = Physics.OverlapSphere(offsetPosition, 1f, layerMask);
            count++;
        }

        //var source = Enemies[Random.Range(0, Enemies.Length)];
        var clone = Instantiate(source, offsetPosition, source.transform.rotation);
        clone.name = source.name;
        clone.transform.parent = this.gameObject.transform.parent.transform;
        if(clone.GetComponent<EnemyBrain>() != null)
            StartCoroutine(enable_brain(clone));
      //  clone.GetComponent<EnemyBrain>().enabled = true;

        spawned = true;

    }

    // Update is called once per frame
   public int oneStepCloser() {
     int currentEnemyCount = 0;
       foreach(Transform child in transform.parent)
       { 
            if(child.tag == "Enemy") {
                if(!child.GetComponent<Enemy>().isDying)
                    currentEnemyCount++;
            }  
        }
         //Debug.Log("current enemy count = " + currentEnemyCount); 
      return currentEnemyCount;
       
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
