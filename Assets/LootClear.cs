using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LootClear : MonoBehaviour
{
    public GameObject[] LootItems;
    public GameObject[] LootWeapons;
    public static LootClear Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Pick(RoomNode room)
    {
        int odds = Random.Range(1, 5);
        if (odds == 1)
        {
            PickWeapon(room);
        } else
        {
            PickItem(room);
        }
    }

    private void PickItem(RoomNode room)
    {
        int item;
        GameObject thisItemLoot;
        item = Random.Range(0, 5);
        thisItemLoot = Instantiate(LootItems[item]) as GameObject;
        thisItemLoot.transform.position = new Vector3 (room.roomCenter.x, room.roomCenter.y, 0);
        Debug.Log("CLEAR Item");
    }

    private void PickWeapon(RoomNode room)
    {
        int weap;
        weap = Random.Range(0, 5);
        GameObject thisWeapLoot = Instantiate(LootWeapons[weap]) as GameObject;
        thisWeapLoot.transform.position = new Vector3 (room.roomCenter.x, room.roomCenter.y, 0);
        Debug.Log("CLEAR Weap");

    }

}
