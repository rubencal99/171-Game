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


    public Vector3[] arcArray;
    GameObject[] dotArray;

    Vector2 minPosition;
    Vector2 maxPosition;
    public int resolution;

    public TileBase normal_tile;
    public TileBase barrier_tile;

    LineRenderer lr;

    
    // Start is called before the first frame update
    public void Start() {

       // boxCol = GetComponent<BoxCollider2D>();
        room = this.transform.parent.GetComponent<RoomNode>();
        lr = this.GetComponent<LineRenderer>();
        //GameObject[] tilesArray =  GameObject.FindGameObjectsWithTag("Tilemap");
       // Tilemap tiles = tilesArray[0].GetComponent<Tilemap>();
        this.gameObject.transform.localScale = new Vector3((float)room.length + 1.0f, 3f, (float)room.width + 1.0f);
        
        minPosition = room.bottomLeftCorner;
        minPosition.x -= boundsOffset; minPosition.y -= boundsOffset;
        maxPosition = room.topRightCorner;
        maxPosition.x += boundsOffset; maxPosition.y += boundsOffset;

       // if(transform.parent.transform.GetComponentInChildren<EnemySpanwer>().Waves.Count > 1)
        //    barriers_on = true;
        // List<Vector2> corner_points = new List<Vector2>()
        // {
        //     room.bottomLeftCorner,
        //     room.topLeftCorner,
        //     room.topRightCorner,
        //     room.bottomRightCorner
        // };
        // edge = GetComponentInChildren<EdgeCollider2D>();
        // edge.SetPoints(corner_points);
        // Debug.Log(edge);
        
        // arcArray = new Vector3[4];
        // dotArray = new GameObject[4];
        // lr.positionCount = 4;

        // for(int i = 0; i < 4; i++) {
        //      dotArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //      dotArray[i].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //      dotArray[i].transform.parent = this.transform;
        //     //. dotArray[i].s
        // }

            // RenderArc();

    }



    // void RenderArc() {

    //     // obsolete: lr.SetVertexCount(resolution + 1);

    //     lr.SetPositions(CalculateArcArray());
    //     //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    // }

    // Vector3[] CalculateArcArray() {
    //     Vector3[] ArcArray = new Vector3[]{
    //         new Vector3(room.bottomLeftCorner.x, 0.5f, room.bottomLeftCorner.y),
    //         new Vector3(room.bottomRightCorner.x, 0.5f, room.bottomRightCorner.y),
    //         new Vector3(room.topRightCorner.x, 0.5f, room.topRightCorner.y),
    //         new Vector3(room.topLeftCorner.x, 0.5f, room.topLeftCorner.y),
    //     };

    //     return ArcArray;
    // }
    
    public bool guarded = false;

    public void toggleGuarded() {
        this.guarded = !this.guarded;
    }

    public void setGuarded(bool val) {
        this.guarded = val;
    }
    /*void OnTriggerEnter2D(Collider2D other) {
       // if(!guarded) {
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                GraphUpdater.SetNewBounds(GetComponent<Collider2D>().bounds);
            //this.GetComponent<Collider2D>().isTrigger = false;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.SetActive(false);
           //  Debug.Log("leaving collider");
            }
     //   }
    }*/

    void OnTriggerEnter(Collider other) {
           //Debug.Log("Just entered room");
            if(other.tag == "Player") {
                this.transform.parent.GetComponent<RoomClearCheck>().setRoomActive();
                Player.instance.currentRoom = room;
                StartCoroutine(WaitToUpdateTiles(barrier_tile));
                //GraphUpdater.SetNewBounds(GetComponent<Collider2D>().bounds);
            //this.GetComponent<Collider2D>().isTrigger = false;
            //this.transform.GetChild(0).gameObject.SetActive(true);
            //this.gameObject.SetActive(false);
           //  Debug.Log("leaving collider");
            }
       
    }

    public IEnumerator WaitToUpdateTiles(TileBase tile) {
        yield return new WaitForSeconds(2);
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

    private void FixedUpdate() {
        Vector3 newLocation = Player.instance.transform.localPosition;
       
        if(guarded) {
            
         if (newLocation.x > maxPosition.x)
         {
             newLocation.x = maxPosition.x;
         }
         else if (newLocation.x < minPosition.x)
         {
             newLocation.x = minPosition.x;
         }
         if (newLocation.z > maxPosition.y)
         {
             newLocation.z = maxPosition.y;
         }
         else if (newLocation.z < minPosition.y)
         {
             newLocation.z = minPosition.y;
         }    

        // Player.instance.transform.localPosition = newLocation;

        }
    }
     void OnTriggerExit(Collider other)
    { 
        if(guarded) {    
            if(other.gameObject.tag == "Player") {
                GetComponent<BoxCollider>().enabled = true;
            } 
        }
    }

    void OnDestroy() {
        UpdateTiles(normal_tile);
    }

    // void OnTriggerExit2D(Collider2D other) {

    //     if(other.tag == "Player") {
           
    //     }
    // //    this.transform.parent.Find("Exit Collider").GetChild(0).gameObject.SetActive(true);
       
    // }
}
