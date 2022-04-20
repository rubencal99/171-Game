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
               enemyCount += child.GetComponent<EnemySpanwer>().enemyCount;
               spawners.Add(child.gameObject);
              }
        }
        // Debug.Log(" Room Enemy count = " + enemyCount + ", spawner count = " + spawners.Count);
    }


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
        //if(enemyCount <= 0 && spawners[0].GetComponent<EnemySpanwer>().Waves.Count <= 0)
        //    checkIfClear();
       
   }
    public void checkIfClear() {
          foreach(Transform child in transform)
        {
        if(child.tag == "Spawner") Destroy(child.gameObject);
         if(child.tag == "Door") child.GetComponent<EntryCollider>().enabled = false;
            
            // GraphUpdater.InRoom = false;
        }

        if(this.GetComponent<RoomNode>().RoomType == "Boss") {
            Debug.Log("Boss defeated");
            exit = GameObject.FindWithTag("Map Gen").GetComponent<MapGenerator>().Exit;

            Vector3 exit_pos = new Vector3((float)this.GetComponent<RoomNode>().roomCenter.x, 0f, (float)this.GetComponent<RoomNode>().roomCenter.y);
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
                   //child.GetChild(0).gameObject.SetActive(true);

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
