using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode
{
    public List<TileNode> tileList= new List<TileNode>();
    public Vector2Int roomCenter;
    public List<RoomNode> NeighborRooms = new List<RoomNode>();

    public void CalculateCenter()
    {
        TileNode firstTile = tileList[0];
        TileNode lastTile = tileList[tileList.Count-1];

        Vector2Int firstPoint = new Vector2Int(firstTile.x, firstTile.y);
        Vector2Int lastPoint = new Vector2Int(lastTile.x, lastTile.y);

        roomCenter = (Vector2Int)((firstPoint + lastPoint) / 2);
    }
}
