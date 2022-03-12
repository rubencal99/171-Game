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
        int item;
        GameObject thisLoot;
        item = Random.Range(0, 3);
        thisLoot = Instantiate(LootOp[item]) as GameObject;
        thisLoot.transform.position = Dead.transform.position;
    }

}
