using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapCheck
{
    public static bool FinalPass(TileNode[,] map, List<CorridorNode> Corridors)
    {
        int count = 1;
        // check every corridor
        foreach(CorridorNode corridor in Corridors)
        {
            // check every tile except first two and last two tiles in corridor
            for(int i = 2; i < corridor.tileList.Count - 2; i++)
            {
                TileNode tile = corridor.tileList[i];
                if(CheckIfBorderingRoom(map, tile))
                {
                    Debug.Log("We've gone through " + count + " corridors.");
                    Debug.Log("Tile at (" + tile.x + ", " + tile.y + ") is bordering a room");
                    return true;
                }
            }
            count++;
        }
        return false;
    }

    private static bool CheckIfBorderingRoom(TileNode[,] map, TileNode tile)
    {
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(map[tile.x - x, tile.y - y].value == 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
