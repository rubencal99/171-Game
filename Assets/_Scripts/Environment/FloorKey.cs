using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKey : MonoBehaviour
{
    void OnCollisionEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("Player has aquired key");
            Player.instance.hasKey = true;
            KillAllEnemies();
            Destroy(gameObject);    
        }
    }

    // Goes through all enemies in the room and hits them for 1000
    void KillAllEnemies()
    {
        Enemy[] enemies = transform.parent.GetComponentsInChildren<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.GetHit(100);  
        }
    }
}
