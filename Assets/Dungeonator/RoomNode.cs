using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public List<TileNode> tileList= new List<TileNode>();
    public Vector2Int roomCenter;
    public int length;
    public int width;
    public int area;
    public string RoomType;
    public int MaxNeighbors;
    public EnemySpanwer spawner;
    public List<RoomNode> NeighborRooms = new List<RoomNode>();
    public List<RoomNode> RoomsByDistance = new List<RoomNode>();

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
        if(RoomType == "Normal")
        {
            MaxNeighbors = 3;
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

    public void CalculateCenter()
    {
        TileNode firstTile = tileList[0];
        TileNode lastTile = tileList[tileList.Count-1];

        Vector2Int firstPoint = new Vector2Int(firstTile.x, firstTile.y);
        Vector2Int lastPoint = new Vector2Int(lastTile.x, lastTile.y);

        roomCenter = (Vector2Int)((firstPoint + lastPoint) / 2);
        length = lastTile.x - firstTile.x;
        width = lastTile.y - firstTile.y;
        area = length * width;
    }
}
