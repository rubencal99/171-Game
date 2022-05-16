using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{   
    public static Loot instance;
    public GameObject[] LootOp;
    public GameObject[] LootSup;

    private void Awake()
    {
        instance = this;
    }

    public void Pick(GameObject Dead)
    {
        int item;
        
        item = Random.Range(0, 3);
        if (item == 2 || item == 3)
        {
            PickSup(Dead);
        }
        if (item == 1)
        {
            PickPwr(Dead);
        }
        
    }

    public void PickSup(GameObject Dead)
    {
        GameObject thisLoot;
        int pK = Random.Range(0, 1);
        thisLoot = Instantiate(LootSup[pK]) as GameObject;
        thisLoot.transform.position = Dead.transform.position;
        Destroy(gameObject);

    }

    public void PickPwr(GameObject Dead)
    {
        GameObject thisLoot;
        int pK = Random.Range(0, 2);
        thisLoot = Instantiate(LootOp[pK]) as GameObject;
        thisLoot.transform.position = Dead.transform.position;
    }

}
