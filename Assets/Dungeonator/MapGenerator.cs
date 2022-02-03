using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private TileSpritePlacer AutoTiler;
    [SerializeField]
    private GameObject LightPrefab;
    public int columns;
    public int rows;
    TileNode[,] map;

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

    public int numLargeRooms;
    public int BossRoom = 1;
    public int normalRooms;
    public int overgrownRooms;

    // Queues for the BSP algorithm
    private Queue<int[]> queue = new Queue<int[]>();
    private Queue<int[]> roomsList = new Queue<int[]>();
    private List<TileNode> roomTiles = new List<TileNode>();


    public TileNode[,] GenerateMap()
    {
        map = new TileNode[columns, rows];

        FillMap();
        BinarySpace();

        DrawMap();

        return map;
    }
    void DrawMap(){
        // AutoTiler.Clear();
        AutoTiler.PaintFloorTiles(roomTiles);
    }

    void FillMap(){
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                map[x, y] = new TileNode();
                map[x, y].x = x;
                map[x, y].y = y;
            }
        }
    }

    void BinarySpace()
    {
        // Instantiate first room space (whole map)
        var temp = new int[4];
        temp[0] = 0;
        temp[1] = 0;
        temp[2] = columns - 1;
        temp[3] = rows - 1;
        // This will be our queue of room spaces
        queue.Enqueue(temp);

        int count = 0;
        // Main loop that splits map
        // Debug.Log("Before Binary Space splitting loop");
        while(queue.Count > 0) 
        {
            // print("Times looped: " + count);
            if (count > 200){
                break;
            }
            // Debug.Log("In Binary Space splitting loop");
            // print("COUNT: " + queue.Count);
            // print("Times looped: " + count);
            int[] space = queue.Dequeue();
            var x1 = space[0];
            var y1 = space[1];
            var x2 = space[2];
            var y2 = space[3];

            // print("x1, y1, x2, y2: " + x1 + ", " + y1 + ", " + x2 + ", " + y2);

            var width = x2 - x1;
            var height = y2 - y1;

            if (width > min_x_BSP && height > min_y_BSP)
            {
                // Randomly choose which split we prefer
                if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    if (height >= min_y_BSP * 2)
                    {
                        SplitHorizontal(space);
                    }
                    else if (width >= min_x_BSP * 2)
                    {
                        SplitVertical(space);
                    }
                    else
                    {
                        roomsList.Enqueue(space);
                    }
                }
                else
                {
                    if (width >= min_x_BSP * 2)
                    {
                        SplitVertical(space);
                    }
                    else if (height >= min_y_BSP * 2)
                    {
                        SplitHorizontal(space);
                    }
                    else
                    {
                        roomsList.Enqueue(space);
                    }
                }
            }
            count++;
        }

        // This is where we create the rooms
        while (roomsList.Count > 0)
        {
            int[] room = roomsList.Dequeue();

            int x1 = (int)room[0];
            int y1 = (int)room[1];
            int x2 = (int)room[2];
            int y2 = (int)room[3];

            // Here we fill the negative space with empty space 
            // I.e. room creation
            for (int i = x1 + 1; i < x2 - 1; i++)
            {
                for (int j = y1 + 1; j < y2 - 1; j++)
                {
                    map[i, j].value = 1;
                    roomTiles.Add(map[i, j]);
                }
            }
            AddLights(x1, y1, x2, y2);
        }
    }

    void AddLights(int x1, int y1, int x2, int y2)
    {
        var lightPrefab1 = Instantiate(LightPrefab, new Vector2(x1 + 4, y1 + 4), Quaternion.identity);
        var lightPrefab2 = Instantiate(LightPrefab, new Vector2(x1 + 4, y2 - 4), Quaternion.identity);
        var lightPrefab3 = Instantiate(LightPrefab, new Vector2(x2 - 4, y1 + 4), Quaternion.identity);
        var lightPrefab4 = Instantiate(LightPrefab, new Vector2(x2 - 4, y2 - 4), Quaternion.identity);
    }

    // Splits map horizontally
    void SplitHorizontal(int[] roomSpace)
    {
        var x1 = roomSpace[0];
        var y1 = roomSpace[1];
        var x2 = roomSpace[2];
        var y2 = roomSpace[3];

        var ySplit = UnityEngine.Random.Range(y1 + 1, y2);
        var room1 = new int[] { x1, y1, x2, ySplit };
        var room2 = new int[] { x1, ySplit + 1, x2, y2 };

        queue.Enqueue(room1);
        queue.Enqueue(room2);
    }

    // Splits map vertically
    void SplitVertical(int[] roomSpace)
    {
        var x1 = roomSpace[0];
        var y1 = roomSpace[1];
        var x2 = roomSpace[2];
        var y2 = roomSpace[3];
        var xSplit = UnityEngine.Random.Range(x1 + 1, x2);
        var room1 = new int[] { x1, y1, xSplit, y2 };
        var room2 = new int[] { xSplit + 1, y1, x2, y2 };

        // print("In vertical.");

        queue.Enqueue(room1);
        queue.Enqueue(room2);
    }

    void AddCorridors()
    {
        
    }

    void OnDrawGizmos()
    {
        /* if (enemies != null)
        {
            foreach (Vector3 enemy in enemies)
            {
                // print("Enemy X: " + enemy.x);
                // print("Enemy Y: " + enemy.y);
                // print("Enemy Z: " + enemy.z);
                Gizmos.DrawSphere(enemy, displayRadius);
            }
        }*/
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // Debug.Log("(x, y) = (" + x + ", " + y+ ")");
                if (map[x, y].value == 0)
                {
                    Gizmos.color = new Color(0, 0, 0, 1f);
                }
                else if (map[x, y].value == 1)
                {
                    Gizmos.color = new Color(0, 255, 0, 1f);
                }
                else
                {
                    Gizmos.color = new Color(0, 255, 255, 1f);
                }

                Gizmos.DrawCube(new Vector3(x, y, 0), new Vector3(1, 1, 1));
            }
        }
    }
}
