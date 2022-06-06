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

    public bool activated = false;

    [SerializeField]
    public List<MinimapIcons> room_icons;

     [System.Serializable]
    public class MinimapIcons
    {
        public Sprite icon;
        public string name;
    }
    
    // Start is called before the first frame update
    public void Start() {

       // boxCol = GetComponent<BoxCollider2D>();
        room = this.transform.parent.GetComponent<RoomNode>();
        //tilemap = TileSpritePlacer.instance.Tilemap;
        //normal_tile = TileSpritePlacer.instance.Tile;
        //barrier_tile = TileSpritePlacer.instance.colliderTile;

        //GameObject[] tilesArray =  GameObject.FindGameObjectsWithTag("Tilemap");
       // Tilemap tiles = tilesArray[0].GetComponent<Tilemap>();
        this.gameObject.transform.localScale = new Vector3((float)room.length - 2.5f, 2.5f, (float)room.width - 2.5f);
        
        minPosition = room.bottomLeftCorner;
        minPosition.x -= boundsOffset; minPosition.y -= boundsOffset;
        maxPosition = room.topRightCorner;
        maxPosition.x += boundsOffset; maxPosition.y += boundsOffset;

        var spr = this.GetComponentInChildren<SpriteRenderer>();
        foreach(var icon in room_icons) {
       
            switch(icon.name) {
                case "boss":
                    if(room.RoomType == "Boss" ) spr.sprite = icon.icon;
                    break;
                case "key":
                    if(room.RoomType == "Key" ) spr.sprite = icon.icon;
                    break;
                case "shop":
                    if(room.RoomType == "Shop" ) spr.sprite = icon.icon;
                    break;
                case "door":
                    if(room.RoomType == "Door" ) spr.sprite = icon.icon;
                    break;
                 case "aux":
                    if(room.RoomType == "Auxiliary" ) spr.sprite = icon.icon;
                    break;
                 case "reward":
                    if(room.RoomType == "Reward" ) spr.sprite = icon.icon;
                    break;
                 
            }

             if(room.RoomType == "Normal" || room.RoomType == "Large" ||  room.RoomType == "Extra") spr.enabled = false;  

        // if(room.RoomType == "Boss" )
        // ( room.RoomType == "Key") spr.color = Color.yellow;
        // if(room.RoomType == "Shop") spr.color = Color.green;
        // //if(room.RoomType == "Reward") spr.color = Color.cyan;
        // if(room.RoomType == "Door") spr.color = Color.magenta;
        // if(room.RoomType == "Auxiliary") spr.color = Color.red;
       
         }
    }


    void OnTriggerEnter(Collider other) {
           //Debug.Log("Just entered room");
            if(other.tag == "Player" && !activated) {
                activated = true;
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                Debug.Log("Room type = " + Player.instance.currentRoom.RoomType);
                if(!(Player.instance.currentRoom.RoomType == "Reward" || Player.instance.currentRoom.RoomType == "Shop")) {
                    Debug.Log("Room type = " + Player.instance.currentRoom.RoomType);
                    StartCoroutine(WaitToUpdateTiles(barrier_tile));
                }
            }
    }

    public IEnumerator WaitToUpdateTiles(TileBase tile) {
        yield return new WaitForSeconds(0.3f);
        UpdateTiles(tile);
    }



    void UpdateTiles(TileBase tile) {
        var firstTile = room.tileList[0, 0];
        TileNode check = MapGenerator.map[0, 0];
        for (int row = (int)room.bottomLeftCorner.x; row <= (int)room.bottomRightCorner.x; row++)
        {
            //uncomment lines + switch .y to .z to enable third dimension

           // var wallPosition = new Vector3(row, 0, room.bottomLeftCorner.y);
           var tilePos = new Vector3Int(row, 0, room.bottomLeftCorner.y);
           var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
        //    Debug.Log("tilepos = " + MapGenerator.map[tilePosition.x + 1, tilePosition.y - 4].value);
           //tilePosition.y = 0;
            if(Helper.IsInMapRange(tilePosition.x, tilePosition.y, ref MapGenerator.map))
            {
                if(MapGenerator.map[tilePosition.x, tilePosition.y].value == 2)
                    tilemap.SetTile(tilePosition, tile);
            }
           
            
        }
        for (int row = (int)room.topLeftCorner.x; row <= (int)room.topRightCorner.x; row++)
        {
            //var wallPosition = new Vector3(row, 0, room.topRightCorner.y);
            var tilePos = new Vector3Int(row, 0, room.topLeftCorner.y);
             var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
            //    Debug.Log("tilepos = " + MapGenerator.map[tilePosition.x + 1, tilePosition.y - 4].value);
            if(Helper.IsInMapRange(tilePosition.x, tilePosition.y, ref MapGenerator.map))
            {
               if(MapGenerator.map[tilePosition.x, tilePosition.y].value == 2)
                tilemap.SetTile(tilePosition, tile);
            }
            
        }
        for (int col = (int)room.bottomLeftCorner.y; col <= (int)room.topLeftCorner.y; col++)
        {
            //var wallPosition = new Vector3(room.bottomLeftCorner.x, 0, col);
            
            var tilePos  = new Vector3Int(room.bottomLeftCorner.x ,0, col);
              
             var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
            //  Debug.Log("tilepos = " + MapGenerator.map[tilePosition.x + 1, tilePosition.y - 4].value);
            if(Helper.IsInMapRange(tilePosition.x, tilePosition.y, ref MapGenerator.map))
            {
               if(MapGenerator.map[tilePosition.x, tilePosition.y].value == 2)
                tilemap.SetTile(tilePosition, tile);
            }
        }
        for (int col = (int)room.bottomRightCorner.y; col <= (int)room.topRightCorner.y; col++)
        {
            //var wallPosition = new Vector3(room.bottomRightCorner.x, 0f, col);
            var tilePos = new Vector3Int(room.bottomRightCorner.x,0, col);
              
              var tilePosition = tilemap.WorldToCell((Vector3)tilePos);
            //   Debug.Log("tilepos = " + MapGenerator.map[tilePosition.x + 1, tilePosition.y - 4].value);
            if(Helper.IsInMapRange(tilePosition.x, tilePosition.y, ref MapGenerator.map))
           {
               if(MapGenerator.map[tilePosition.x, tilePosition.y].value == 2)
                tilemap.SetTile(tilePosition, tile);
           }
        }
    }


    void OnDisable() {
        StartCoroutine(WaitToUpdateTiles(normal_tile));
    }

}
