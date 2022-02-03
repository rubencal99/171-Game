using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int columns;
    public int rows;

    // Min dimensions of rooms
    public int min_x_BSP;
    public int min_y_BSP;

    public bool UseCellular;
    public string Seed;
    public bool useRandomSeed;
    [Range(0, 100)]
    public int wallPercent;
    public int wallCount;
    
    public int Iterations;

    // Queues for the BSP algorithm
    private Queue<int[]> queue = new Queue<int[]>();
    private Queue<int[]> roomsList = new Queue<int[]>();


    public int[,] GenerateMap()
    {
        Debug.Log("Not implemented yet.");
        return null;
    }
}
