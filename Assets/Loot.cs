using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{   
    public static Loot instance;
    public GameObject[] LootOp;

    private void Awake()
    {
        instance = this;
    }

    public void Pick(GameObject Dead)
    {
        /*Vector3 offsetPosition = transform.position;
        offsetPosition.x += Random.Range(-5f, 5f);
        offsetPosition.y += Random.Range(-5f, 5f);8*/
        int item;
        GameObject thisLoot;
        item = Random.Range(0, 6);
        thisLoot = Instantiate(LootOp[item]) as GameObject;
        thisLoot.transform.position = Dead.transform.position;
    }

}
