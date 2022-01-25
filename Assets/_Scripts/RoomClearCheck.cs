using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClearCheck : MonoBehaviour
{

    private int enemyCount = 0;
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = spawner.GetComponent<EnemySpanwer>().numToSpawn;
        Debug.Log("Enemy count = " + enemyCount);
    }

    // Update is called once per frame
   public void oneStepCloser() {
       enemyCount --;
       Debug.Log("One step closer, " + enemyCount + "enemies remaining");
       checkIfClear();
       
   }
    public void checkIfClear() {
        if(enemyCount <= 0) {
               foreach(Transform child in transform)
                {
                if(child.tag == "Spawner")
                    Destroy(child.gameObject);
                }

                Debug.Log("room cleared");
 
        }

    }
}
