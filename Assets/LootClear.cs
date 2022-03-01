using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LootClear : MonoBehaviour
{
    public GameObject[] LootItems;
    public GameObject[] LootWeapons;

    public void Pick()
    {
        int odds = Random.Range(1, 5);
        if (odds == 1)
        {
            PickWeapon();
        } else
        {
            PickItem();
        }
    }

    private void PickItem()
    {
        Vector3 setPosition = transform.position;
        setPosition.x += 0;
        setPosition.y += 0;

        int item;
        GameObject thisItemLoot;
        item = Random.Range(0, 5);
        thisItemLoot = Instantiate(LootItems[item]) as GameObject;
        GameObject x = GameObject.Find("Floor");
        Tilemap cent = x.GetComponent(typeof(Tilemap)) as Tilemap;
        Vector3 center = cent.cellBounds.center;
        thisItemLoot.transform.position = center; ;
        Debug.Log("CLEAR Item");
    }

    private void PickWeapon()
    {
        Vector3 setPosition = transform.position;
        setPosition.x = 0;
        setPosition.y = 0;

        int weap;
        weap = Random.Range(0, 5);
        GameObject thisWeapLoot = Instantiate(LootWeapons[weap]) as GameObject;
        GameObject x = GameObject.Find("Floor");
        Tilemap cent = x.GetComponent(typeof(Tilemap)) as Tilemap;
        Vector3 center = cent.cellBounds.center;
        thisWeapLoot.transform.position = center;
        Debug.Log("CLEAR Weap");

    }

}
