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
        if (odds >= 1)
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
        item = Random.Range(0, 1);
        thisItemLoot = Instantiate(LootItems[item]) as GameObject;
        thisItemLoot.transform.position = CalculateSpawn(room);
        Debug.Log("CLEAR Item");
    }

    private void PickWeapon(RoomNode room)
    {
        int weap;
        weap = Random.Range(0, 5);
        GameObject thisWeapLoot = Instantiate(LootWeapons[weap]) as GameObject;
        thisWeapLoot.transform.position = CalculateSpawn(room);
        Debug.Log("CLEAR Weap");

    }

    private Vector3 CalculateSpawn(RoomNode room)
    {
        Vector3 spawnPosition;
        if(room.FindTileByPoint(room.roomCenter.x, room.roomCenter.y).value != 1)
        {
            var tile = room.GrabValidTile();
            spawnPosition = new Vector3(tile.x, tile.y, 0);
        }
        else
        {
            spawnPosition = new Vector3(room.roomCenter.x, room.roomCenter.y, 0);
        }
        return spawnPosition;
    }

}
