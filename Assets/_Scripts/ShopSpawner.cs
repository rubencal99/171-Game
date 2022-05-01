using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : EnemySpanwer
{

    // Start is called before the first frame update
    void Start()
    {
        thisroom = transform.parent.gameObject.GetComponent<RoomNode>();
       // enemyCount = this.numToSpawn;
        // Debug.Log("initial Enemy count = " + enemyCount);
       // Enemies = Resources.LoadAll<GameObject>("_Prefabs/Enemies");


        //SpawnObject();
    }

    // void Update() {
    //     if(spawned)
    //         oneStepCloser();
    // }

    new public void SpawnObject(){
        Vector3 offsetPosition = new Vector3(thisroom.roomCenter.x, 1, thisroom.roomCenter.y);
        //offsetPosition.x += Random.Range(-offset, offset);
        //offsetPosition.y += Random.Range(-offset, offset);

       // var source = Enemies[Random.Range(0, Enemies.Count)];
        var source = Waves[0].Enemies[0];
        var clone = Instantiate(source.Enemy, offsetPosition, Quaternion.identity);
        clone.name = source.Enemy.name;
        clone.transform.parent = this.gameObject.transform.parent.transform;

        // PlayerInput Player = clone.GetComponent<Shop>().Player.GetComponent<PlayerInput>();
        PlayerInput Player = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        Player.ShopKeeper = clone.GetComponent<Shop>();

        spawned = true;

    }

    // Update is called once per frame
   /*public void oneStepCloser() {
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
 
//     }*/
    
 }
