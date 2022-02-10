using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public List<TileNode> tileList= new List<TileNode>();
    public Vector2Int roomCenter;
    public string RoomType;
    public int MaxNeighbors;
    public List<RoomNode> NeighborRooms = new List<RoomNode>();
    public List<RoomNode> RoomsByDistance = new List<RoomNode>();

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
    }
}
