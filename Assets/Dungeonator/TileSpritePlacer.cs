using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSpritePlacer : MonoBehaviour
{
    [SerializeField]
    private Tilemap Tilemap;

    // We can create an array of TileBase for multiple tiles
    [SerializeField]
    private TileBase Tile;

    private List<TileNode> prevNodes;

    public void PaintFloorTiles(List<TileNode> tileNodes)
    {
        Clear();
        PaintTiles(tileNodes, Tilemap, Tile);
    }

    private void PaintTiles(List<TileNode> tileNodes, Tilemap tilemap, TileBase tile)
    {
        // Debug.Log("Number of room tiles: " + tileNodes.Count);
        Vector2 position = Vector2.zero;
        foreach (TileNode tilenode in tileNodes)
        {
            position.x = tilenode.x;
            position.y = tilenode.y;
            PaintSingleTile(tilemap, tile, position);
        }
        prevNodes = tileNodes;
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2 position)
    {
        // Debug.Log("In paint tiles");
        // Debug.Log("Position: (" + position.x  + ", " + position.y + ")");
        var tilePosition = tilemap.WorldToCell((Vector3)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        Debug.Log("In Clear");
        if (prevNodes != null)
        {
            Vector2 position = Vector2.zero;
            foreach (TileNode tilenode in prevNodes)
            {
                position.x = tilenode.x;
                position.y = tilenode.y;
                var tilePosition = Tilemap.WorldToCell((Vector3)position);
                Tilemap.SetTile(tilePosition, null);
            }
        }
        // Tilemap.ClearAllTiles();
    }
}
