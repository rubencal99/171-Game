using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class Helper
{
    public static int passageSize = 1;

    public static void ConnectClosestRooms(ref List<RoomNode> allRooms, ref TileNode[,] map)
    {

    }

    public static CorridorNode CreatePassage(RoomNode roomA, RoomNode roomB, TileNode tileA, TileNode tileB, ref TileNode[,] map)
    {
        // RoomNode.ConnectRooms(roomA, roomB);
        Debug.DrawLine(TileNodeToWorldPoint(tileA, ref map), TileNodeToWorldPoint(tileB, ref map), Color.green, 1);

        List<TileNode> line = GetLine(tileA, tileB, ref map);

        if (line == null)
        {
            return null;
        }

        List<TileNode> tileList = new List<TileNode>();

        foreach (TileNode c in line)
        {
            DrawCircle(tileList, c, passageSize, ref map);
        }

        CorridorNode corridor = new CorridorNode();
        corridor.tileList = tileList;
        corridor.TargetRoomList.Add(roomA);
        corridor.TargetRoomList.Add(roomB);
        corridor.ConnectedRoomList.Add(roomA);
        corridor.ConnectedRoomList.Add(roomB);

        if(corridor.IsBorderingRoom())
        {
            corridor.NullifyCorridor();
            return null;
        }

        return corridor;
    }

    // this function merges all the Connected Rooms of both rooms together 
    public static void MergeNeighbors(RoomNode roomA, RoomNode roomB)
    {
        roomA.ConnectedRooms.Union<RoomNode>(roomB.ConnectedRooms);
        roomB.ConnectedRooms.Union<RoomNode>(roomA.ConnectedRooms);
    }

    public static void DrawCircle(List<TileNode> tileList, TileNode c, int r, ref TileNode[,] map)
    {
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int drawX = c.x + x;
                    int drawY = c.y + y;
                    if (IsInMapRange(drawX, drawY, ref map) && map[drawX, drawY].value == 0)
                    {
                        map[drawX, drawY].value = 2;
                        tileList.Add(map[drawX, drawY]);
                    }
                }
            }
        }
    }

    // Checks to see if [x, y] is in map
    public static bool IsInMapRange(int x, int y, ref TileNode[,] map)
    {
        int columns = map.GetLength(0);
        int rows = map.GetLength(1);
        return x >= 0 && x < columns && y >= 0 && y < rows;
    }

    public static List<TileNode> GetLine(TileNode from, TileNode to, ref TileNode[,] map)
    {
        List<TileNode> line = new List<TileNode>();

        int x = from.x;
        int y = from.y;

        int dx = to.x - from.x;
        int dy = to.y - from.y;

        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);

        int longest = Mathf.Abs(dx);
        int shortest = Mathf.Abs(dy);

        if (longest < shortest)
        {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);

            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }

        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i++)
        {
            if(map[x, y].value == 0)
            {
                line.Add(map[x, y]);
            }

            // this checks if the corridor is bulldozing through another room
            if(map[x, y].value == 1 && map[x, y].room != from.room && map[x, y].room != to.room)
            {
                return null;
            }

            if (inverted)
            {
                y += step;
            }
            else
            {
                x += step;
            }
            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest)
            {
                if (inverted)
                {
                    x += gradientStep;
                }
                else
                {
                    y += gradientStep;
                }
                gradientAccumulation -= longest;
            }
        }
        return line;
    }

    public static Vector3 TileNodeToWorldPoint(TileNode tile, ref TileNode[,] map)
    {
        return new Vector3(tile.x, tile.y, 0);
    }
}
