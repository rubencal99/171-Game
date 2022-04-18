using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomNode : MonoBehaviour
{
    // public List<TileNode> tileList= new List<TileNode>();
    public TileNode[,] tileList;
    public int tileCount = 0;
    public TileNode CenterTile;
    public Vector2Int roomCenter;
    public Vector2Int bottomLeftCorner;

    public Vector2Int bottomRightCorner;

    public Vector2Int topRightCorner;

    public Vector2Int topLeftCorner;
    public int length;
    public int width;
    public int area;
    public string RoomType;
    public int MaxNeighbors;
    public bool isAccessibleFromStart;
    public EnemySpanwer spawner;
    public List<RoomNode> NeighborRooms = new List<RoomNode>();
    public List<RoomNode> ConnectedRooms = new List<RoomNode>();
    public List<RoomNode> RoomsByDistance = new List<RoomNode>();
    public int DistanceFromStart;

    public void CreateRoomObject()
    {
        // GameObject tempRoom = new GameObject(roomCenter.ToString());
        // transform.parent = tempRoom.transform;
    }

    public RoomNode()
    {

    }

    public RoomNode(string roomType)
    {
        RoomType = roomType;
        if (RoomType == "Start" || RoomType == "Reward")
        {
            MaxNeighbors = 1;
        }
        if (RoomType == "Boss")
        {
            MaxNeighbors = 2;
        }
        if(RoomType == "Normal" || RoomType == "Auxiliary")
        {
            MaxNeighbors = 2;
        }
        if(RoomType == "Large")
        {
            MaxNeighbors = 4;
        }
    }

    public int NeighborCount {
        get {return NeighborRooms.Count;}
        set{}
    }

    public void AddDimensions(int l, int w)
    {
        length = l;
        width = w;
        tileList = new TileNode[length, width];
        DistanceFromStart = -1;
    }

    public void CalculateCenter()
    {
        TileNode firstTile = tileList[0, 0];
        TileNode lastTile = tileList[length - 1, width - 1];

        Vector2Int firstPoint = new Vector2Int(firstTile.x, firstTile.y);
        Vector2Int lastPoint = new Vector2Int(lastTile.x, lastTile.y);

        //roomCenter = (Vector2Int)((firstPoint + lastPoint) / 2);
        // CenterTile = tileList[(int)tileList.Count/2];
        //roomCenter = new Vector2Int(CenterTile.x, CenterTile.y);
        roomCenter = new Vector2Int((firstPoint.x + lastPoint.x) / 2, (firstPoint.y + lastPoint.y) / 2);
        
        foreach(TileNode tile in tileList)
        {
            Vector2Int tilePosition = new Vector2Int(tile.x, tile.y);
            if(tilePosition == roomCenter)
            {
                CenterTile = tile;
            }
        }
        if(CenterTile == null)
        {
            Debug.Log("Center tile not found");
            CenterTile = tileList[(int)(length/2), (int)(width/2)];
        }
        
        length = (lastTile.x + 1) - (firstTile.x - 1);
        width = (lastTile.y + 1) - (firstTile.y -1);
        area = length * width;

        CalculateCorners();
    }

    void CalculateCorners()
    {
  
        bottomLeftCorner.x = roomCenter.x - (int)Mathf.Ceil(length / 2);
        bottomLeftCorner.y = roomCenter.y - (int)Mathf.Ceil(width / 2);

        bottomRightCorner.x = roomCenter.x + (int)Mathf.Ceil(length / 2);
        bottomRightCorner.y = roomCenter.y - (int)Mathf.Ceil(width / 2);
        if(length % 2 != 0) bottomRightCorner.x++;

        topLeftCorner.x = roomCenter.x - (int)Mathf.Ceil(length / 2);
        topLeftCorner.y = roomCenter.y + (int)Mathf.Ceil(width / 2);

        topRightCorner.x = roomCenter.x + (int)Mathf.Ceil(length / 2);
        topRightCorner.y = roomCenter.y + (int)Mathf.Ceil(width / 2);
        if(length % 2 != 0) topRightCorner.x++;

    }

    public TileNode GrabValidTile()
    {
        Vector2Int r = new Vector2Int(Random.Range(roomCenter.x - (width /2) + 3, roomCenter.x + (width /2) - 3),
                                        Random.Range(roomCenter.y - (length /2) + 3, roomCenter.y + (length /2) - 3));

        var count = 0;
        //Debug.Log("Room Center: " + roomCenter);
        //Debug.Log("Tile location: " + r);
        TileNode tile = FindTileByPoint(r.x, r.y);
        while(tile == null || tile.value != 1)
        {
            if(count >= 3)
            {
                Debug.Log("Couldn't find valid tile.");
                tile = FindTileByPoint(roomCenter.x, roomCenter.y + 6);
                break;
            }
            r = new Vector2Int(Random.Range(roomCenter.x - (width /2) + 3, roomCenter.x + (width /2) - 3),
                                Random.Range(roomCenter.y - (length /2) + 3, roomCenter.y + (length /2) - 3));
            tile = FindTileByPoint(r.x, r.y);
            count++;
        }
        return tile;
    }

    public TileNode FindTileByPoint(int x, int y)
    {
        for(int i = 0; i < length - 1; i++)
        {
            for(int j = 0; j < width - 1; j++)
            {
                if(tileList[i, j].x == x && tileList[i, j].y == y)
                {
                    return tileList[i, j];
                }
            }
        }
        return null;
    }

    public void SetAccessibleFromStart()
    {
        if (!isAccessibleFromStart)
        {
            isAccessibleFromStart = true;
            foreach (RoomNode connectedRoom in ConnectedRooms)
            {
                connectedRoom.SetAccessibleFromStart();
            }
        }
    }

    // this function merges all the Connected Rooms of both rooms together 
    public static void ConnectRooms(RoomNode roomA, RoomNode roomB)
    {
        if (roomA.isAccessibleFromStart)
        {
            roomB.SetAccessibleFromStart();
        }
        else if (roomB.isAccessibleFromStart)
        {
            roomA.SetAccessibleFromStart();
        }

        roomA.NeighborRooms.Add(roomB);
        roomB.NeighborRooms.Add(roomA);

        roomA.ConnectedRooms = roomA.ConnectedRooms.Union<RoomNode>(roomB.ConnectedRooms).ToList<RoomNode>();
        roomB.ConnectedRooms = roomB.ConnectedRooms.Union<RoomNode>(roomA.ConnectedRooms).ToList<RoomNode>();
        roomA.ConnectedRooms.Add(roomB);
        roomB.ConnectedRooms.Add(roomA);

        foreach(RoomNode room1 in roomA.ConnectedRooms)
        {
            roomB.ConnectedRooms = roomB.ConnectedRooms.Union<RoomNode>(room1.ConnectedRooms).ToList<RoomNode>();
            room1.ConnectedRooms = room1.ConnectedRooms.Union<RoomNode>(roomB.ConnectedRooms).ToList<RoomNode>();
        }
        foreach(RoomNode room2 in roomB.ConnectedRooms)
        {
            roomA.ConnectedRooms = roomA.ConnectedRooms.Union<RoomNode>(room2.ConnectedRooms).ToList<RoomNode>();
            room2.ConnectedRooms = room2.ConnectedRooms.Union<RoomNode>(roomA.ConnectedRooms).ToList<RoomNode>();
        }
    }

    public bool IsConnected(RoomNode otherRoom)
    {
        return ConnectedRooms.Contains(otherRoom);
    }

    public int CompareTo(RoomNode otherRoom)
    {
        return otherRoom.tileCount.CompareTo(tileCount);
    }
}
