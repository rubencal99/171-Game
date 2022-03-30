using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClearCheck : MonoBehaviour
{
    public GameObject[] Loot;
    private GameObject exit;
    private int enemyCount;
    private List<GameObject> spawners = new List<GameObject>();
    RoomNode room;
    // Start is called before the first frame update
    void Start()
    {
        room = transform.GetComponent<RoomNode>();
        foreach(Transform child in transform) {
           if(child.tag == "Spawner")
           {
               child.GetComponent<EnemySpanwer>().enabled = true;
               enemyCount += child.GetComponent<EnemySpanwer>().numToSpawn;
               spawners.Add(child.gameObject);
              }
        }
        // Debug.Log(" Room Enemy count = " + enemyCount + ", spawner count = " + spawners.Count);
    }

     void Update() {
         // Debug.Log("hello " +  spawners.Count);
         foreach(GameObject sp in spawners)
         {
            if (sp!= null)
            {
                // Debug.Log("finished spawning? " + sp.GetComponent<EnemySpanwer>().spawned);
                if(sp.GetComponent<EnemySpanwer>().spawned)
                    oneStepCloser();
            }
         }
    }


    // Update is called once per frame
//    public void oneStepCloser() {
//        enemyCount --;
//        Debug.Log("One step closer, " + enemyCount + "enemies remaining");
//        checkIfClear();
       
//    }

 public void oneStepCloser() {
      enemyCount = 0;
       foreach(Transform child in transform)
       { 
           if(child.tag == "Enemy") {
               if(!child.GetComponent<Enemy>().isDying)
                    enemyCount++;
           }
            // Debug.Log("current enemy count = " + enemyCount); 
        }
        if(enemyCount <= 0)
            checkIfClear();
       
   }
    public void checkIfClear() {
          foreach(Transform child in transform)
        {
        if(child.tag == "Spawner" || child.tag == "Door")
            Destroy(child.gameObject);
            // GraphUpdater.InRoom = false;
        }

        if(this.GetComponent<RoomNode>().RoomType == "Boss") {
            Debug.Log("Boss defeated");
            exit = GameObject.FindWithTag("Map Gen").GetComponent<MapGenerator>().Exit;

            Vector3 exit_pos = new Vector3((float)this.GetComponent<RoomNode>().roomCenter.x, (float)this.GetComponent<RoomNode>().roomCenter.y, 0f);
            //exit.transform.position = exit_pos;
            Instantiate(exit, exit_pos, Quaternion.identity);
        }

        LootClear thisLoot = LootClear.Instance;
        thisLoot?.Pick(room);
        Debug.Log("room cleared");
 
    }

    public void setRoomActive() {
         Debug.Log("room set active");
        foreach(Transform child in transform) {
           if(child.tag == "Door"){
                   child.GetChild(0).gameObject.SetActive(true);
                   child.GetComponent<EntryCollider>().toggleGuarded();
           }
           if(child.tag == "Spawner")
               child.gameObject.SetActive(true);
               // child.GetComponent<EnemySpanwer>().enabled = true;
                //   foreach(Transform grandchild in transform)
                //     grandchild.GetComponent<EnemyBrain>().enabled = true;
        //     if(child.tag == "Enemy") {
        //        child.GetComponent<EnemyBrain>().enabled = true;
        //    }   
              }
          
    }
}
