using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClearCheck : MonoBehaviour
{

    private int enemyCount;
    private GameObject[] spawners;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform) {
           if(child.tag == "Spawner")
           {
               // child.GetComponent<EnemySpanwer>().enabled = true;
               enemyCount += child.GetComponent<EnemySpanwer>().enemyCount;
              }
        }
        Debug.Log("Enemy count = " + enemyCount);
    }



    // Update is called once per frame
   public void oneStepCloser() {
       enemyCount --;
       Debug.Log("One step closer, " + enemyCount + "enemies remaining");
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
