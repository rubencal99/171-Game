using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using  UnityEngine.Tilemaps;


public class EntryCollider : MonoBehaviour
{
    public BoxCollider boxCol;

    public float boundsOffset = 0.5f;

    public Tilemap tilemap;

    RoomNode room;

    Vector2 minPosition;
    Vector2 maxPosition;
    public int resolution;

    public TileBase normal_tile;
    public TileBase barrier_tile;


    
    // Start is called before the first frame update
    public void Start() {

       // boxCol = GetComponent<BoxCollider2D>();
        room = this.transform.parent.GetComponent<RoomNode>();
        //GameObject[] tilesArray =  GameObject.FindGameObjectsWithTag("Tilemap");
       // Tilemap tiles = tilesArray[0].GetComponent<Tilemap>();
        this.gameObject.transform.localScale = new Vector3((float)room.length, 3f, (float)room.width);
        
        minPosition = room.bottomLeftCorner;
        minPosition.x -= boundsOffset; minPosition.y -= boundsOffset;
        maxPosition = room.topRightCorner;
        maxPosition.x += boundsOffset; maxPosition.y += boundsOffset;

        var spr = this.GetComponentInChildren<SpriteRenderer>();
        if(room.RoomType == "Boss" || room.RoomType == "Key") spr.color = Color.yellow;
        if(room.RoomType == "Shop") spr.color = Color.green;
        if(room.RoomType == "Reward") spr.color = Color.cyan;
        if(room.RoomType == "Door") spr.color = Color.magenta;
        if(room.RoomType == "Auxiliary") spr.color = Color.red;
        if(room.RoomType == "Normal" || room.RoomType == "Large") spr.enabled = false;  
       
    }


    void OnTriggerEnter(Collider other) {
           //Debug.Log("Just entered room");
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                if(transform.parent.gameObject.GetComponentInChildren<EnemySpanwer>().Waves.Count > 1 || room.RoomType == "Boss")
                 StartCoroutine(WaitToUpdateTiles(barrier_tile));
               
            }
    }

    public IEnumerator WaitToUpdateTiles(TileBase tile) {
        yield return new WaitForSeconds(1);
        UpdateTiles(tile);
    }


    void UpdateTiles(TileBase tile) {
        for (int row = (int)room.bottomLeftCorner.x; row <= (int)room.bottomRightCorner.x; row++)
        {
            //uncomment lines + switch .y to .z to enable third dimension

           // var wallPosition = new Vector3(row, 0, room.bottomLeftCorner.y);
           var tilePos = new Vector3Int(row, 0, room.bottomLeftCorner.y);
           var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
           tilemap.SetTile(tilePosition, tile);
            
        }
        for (int row = (int)room.topLeftCorner.x; row <= (int)room.topRightCorner.x; row++)
        {
            //var wallPosition = new Vector3(row, 0, room.topRightCorner.y);
            var tilePos = new Vector3Int(row, 0, room.topRightCorner.y);
             var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
           tilemap.SetTile(tilePosition, tile);
            
        }
        for (int col = (int)room.bottomLeftCorner.y; col <= (int)room.topLeftCorner.y; col++)
        {
            //var wallPosition = new Vector3(room.bottomLeftCorner.x, 0, col);
            var tilePos  = new Vector3Int(room.bottomLeftCorner.x ,0, col);
             var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
           tilemap.SetTile(tilePosition, tile);
        }
        for (int col = (int)room.bottomRightCorner.y; col <= (int)room.topRightCorner.y; col++)
        {
            //var wallPosition = new Vector3(room.bottomRightCorner.x, 0f, col);
            var tilePos = new Vector3Int(room.bottomRightCorner.x,0, col);
              var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
           tilemap.SetTile(tilePosition, tile);
        }
    }


    void OnDisable() {
        UpdateTiles(normal_tile);
    }

}
