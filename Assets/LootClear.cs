using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LootClear : MonoBehaviour
{
    public GameObject Key;
    public GameObject Door;
    public GameObject[] LootItems;
    public GameObject[] LootWeapons;
    public GameObject[] LootAugs;
    public static LootClear Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Pick(RoomNode room)
    {
        int odds = Random.Range(1, 10);
        if(room.RoomType == "Key")
        {
            PickKey(room);
        }
        else if(room.RoomType == "Door")
        {
            PickDoor(room);
        }
        else if (odds >= 1 && odds <= 3)
        {
            PickWeapon(room);
        } 
        else if (odds >= 5 && odds <=6)
        {
            PickAug(room);
        }
        else
        {
            PickItem(room);
        }
    }

    private void PickKey(RoomNode room)
    {
        GameObject thisItemLoot = Instantiate(Key) as GameObject;
        thisItemLoot.transform.position = CalculateSpawn(room);
    }
    private void PickDoor(RoomNode room)
    {
        GameObject thisItemLoot = Instantiate(Door) as GameObject;
        thisItemLoot.transform.position = CalculateSpawn(room);
    }

    private void PickItem(RoomNode room)
    {
        int item;
        GameObject thisItemLoot;
        item = Random.Range(0, 1);
        thisItemLoot = Instantiate(LootItems[item]) as GameObject;
        thisItemLoot.transform.position = CalculateSpawn(room);
        Destroy(thisItemLoot, 5);
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

    private void PickAug(RoomNode room)
    {
        int aug;
        aug = Random.Range(0, 25);
        GameObject thisAugLoot = Instantiate(LootAugs[aug]) as GameObject;
        thisAugLoot.transform.position = CalculateSpawn(room);
        Debug.Log("CLEAR Weap");
    }

    private Vector3 CalculateSpawn(RoomNode room)
    {
        Vector3 spawnPosition;
        if(room.FindTileByPoint(room.roomCenter.x, room.roomCenter.y).value != 1)
        {
            var tile = room.GrabValidTile();
            spawnPosition = new Vector3(tile.x, 3, tile.y);
        }
        else
        {
            spawnPosition = new Vector3(room.roomCenter.x, 1, room.roomCenter.y);
        }
        return spawnPosition;
    }

}
