using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Random=UnityEngine.Random;


public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject Grid;
    [SerializeField]
    private bool hasBoss;
    [SerializeField]
    private TileSpritePlacer AutoTiler;
    [SerializeField]
    private GameObject LightPrefab;
    [SerializeField]
    private GameObject ShopKeeper;
    [SerializeField]
    private List<GameObject> Spawner;
    [SerializeField]
    private GameObject BossSpawner;
    [SerializeField]
    private GameObject ShopKeeperSpawner;
    [SerializeField]
    private GameObject PuzzleSpawner;
    [SerializeField]
    private GameObject EntryCollider;
    [SerializeField]
    public GameObject Exit;
    [SerializeField]
    public GameObject SpawnWeapon;

    [SerializeField]
    private GameObject wallVertical, wallHorizontal;
     [SerializeField]
    public placecontrols controls;
    public int columns;
    public int rows;
    public static TileNode[,] map;

    // Min dimensions of rooms
    public Vector2Int minRoomDim;
    public Vector2Int minEntryDim;
    public Vector2Int maxEntryDim;

    public Vector2Int minNormalDim;
    public Vector2Int maxNormalDim;

    public Vector2Int minLargeDim;
    public Vector2Int maxLargeDim;

    public Vector2Int minRewardDim;
    public Vector2Int maxRewardDim;
    [Range(0, 100)]
    public int rewardPercent;
    [Range(0, 100)]
    public int auxiliaryPercent;

    public Vector2Int minBossDim;
    public Vector2Int maxBossDim;

    public bool UseCellular;
    public string Seed;
    public bool useRandomSeed;
    [Range(0, 100)]
    public int wallPercent;
    public int wallCount;
    
    public int Iterations;

    public bool HasEntry = false;
    public bool HasBoss = false;
    public int numLargeRooms;
    public int NumBossRooms = 1;
    public int NumNormalRooms;
    public int overgrownRooms;

    // Queues for the BSP algorithm
    private List<int[]> queue = new List<int[]>();
    private Queue<Tuple<int[], string>> roomsList = new Queue<Tuple<int[], string>>();
    private List<TileNode> roomTiles = new List<TileNode>();
    private List<RoomNode> Rooms = new List<RoomNode>();
    private List<CorridorNode> Corridors = new List<CorridorNode>();

    //private AstarPath AStar;

    private RoomNode StartRoom;
    private RoomNode EndRoom;
    private RoomNode ShopRoom;
    private RoomNode DoorRoom;

    public TileNode[,] GenerateMap()
    {
        map = new TileNode[columns, rows];
        //Debug.Log("Before Everything");
        //Grid.transform.Rotate(Vector3.right * 90);
        //Debug.Log("Grid rotation = " + Grid.transform.localRotation);

        FillMap();
        BinarySpace();

        DrawMap();
        AstarPath.active.Scan();
        //Debug.Log("After draw map");
        Grid.transform.Rotate(Vector3.right * 90);
        
        // AStar = GameObject.FindGameObjectWithTag("AStar").GetComponent<AstarPath>();
        AstarPath.active.Scan();

        //OnDrawGizmos();
        //Debug.Log("Before rotation");
        //Grid.transform.localRotation = Quaternion.Euler(90, 0, 0);
        //Debug.Log("Grid rotation = " + Grid.transform.rotation);

        return map;
    }
    void DrawMap(){
        AutoTiler.Clear();
        AutoTiler.PaintFloorTiles(roomTiles);
        AutoTiler.PaintCollisions(roomTiles, map);
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

    async void BinarySpace()
    {
        // Instantiate first room space (whole map)
        var temp = new int[4];
        temp[0] = 0;
        temp[1] = 0;
        temp[2] = columns - 1;
        temp[3] = rows - 1;
        // This will be our queue of room spaces
        queue.Add(temp);

        int count = 0;
        // Main loop that splits map
        // Debug.Log("Before Binary Space splitting loop");
        while(queue.Count > 0) 
        {
            // print("Times looped: " + count);
            if (count > 200){
                Debug.Log("Too many rooms created");
                break;
            }
            // Debug.Log("In Binary Space splitting loop");
            // print("COUNT: " + queue.Count);
            // print("Times looped: " + count);
            int[] space;
            if (HasEntry)
            {
                // note: This is not optimal, very slow
                space = queue[0];
                queue.RemoveAt(0);
            }
            else
            {
                space = queue[queue.Count - 1];
                queue.RemoveAt(queue.Count - 1);
            }
            var x1 = space[0];
            var y1 = space[1];
            var x2 = space[2];
            var y2 = space[3];

            // print("x1, y1, x2, y2: " + x1 + ", " + y1 + ", " + x2 + ", " + y2);

            var width = x2 - x1;
            var height = y2 - y1;

            var r = UnityEngine.Random.Range(0, 100);
            bool successfulLarge = false;
            bool successfulNormal = false;
            bool successfulReward = false;
            // This gives us larger rooms
            /*if (!successfulLarge)
            {
                if (width > minLargeDim.x && height > minLargeDim.y)
                {
                    // Randomly choose which split we prefer
                    if (UnityEngine.Random.Range(0, 100) < 50)
                    {
                        if (height >= minNormalDim.y * 2)
                        {
                            SplitHorizontal(space);
                        }
                        else if (width >= minNormalDim.x * 2)
                        {
                            SplitVertical(space);
                        }
                        else
                        {
                            roomsList.Enqueue(new Tuple<int[], string>(space, "Large"));
                        }
                    }
                    else
                    {
                        if (width >= minNormalDim.x * 2)
                        {
                            SplitVertical(space);
                        }
                        else if (height >= minNormalDim.y * 2)
                        {
                            SplitHorizontal(space);
                        }
                        else
                        {
                            roomsList.Enqueue(new Tuple<int[], string>(space, "Large"));
                        }
                    }
                    successfulLarge = true;
                }
            }*/
            // This gives us normal rooms
            if (!successfulLarge)
            {
                if (width > minNormalDim.x && height > minNormalDim.y)
                {
                    Debug.Log("In BSP Split normal");
                    Debug.Log("Space Width: " + width);
                    Debug.Log("Space Height: " + height);
                    // Randomly choose which split we prefer
                    if (UnityEngine.Random.Range(0, 100) < 50)
                    {
                        if (height >= minNormalDim.y * 2)
                        {
                            SplitHorizontal(space);
                        }
                        else if (width >= minNormalDim.x * 2)
                        {
                            SplitVertical(space);
                        }
                        else
                        {
                            if(UnityEngine.Random.Range(0, 100) > auxiliaryPercent)
                            {
                                Debug.Log("Decided to create room");
                                roomsList.Enqueue(new Tuple<int[], string>(space, "Normal"));
                            }
                            else
                            {
                                roomsList.Enqueue(new Tuple<int[], string>(space, "Auxiliary"));
                            }
                        }
                    }
                    else
                    {
                        if (width >= minNormalDim.x * 2)
                        {
                            SplitVertical(space);
                        }
                        else if (height >= minNormalDim.y * 2)
                        {
                            SplitHorizontal(space);
                        }
                        else
                        {
                            if(UnityEngine.Random.Range(0, 100) > auxiliaryPercent)
                            {
                                Debug.Log("Decided to create room");
                                roomsList.Enqueue(new Tuple<int[], string>(space, "Normal"));
                            }
                            else
                            {
                                roomsList.Enqueue(new Tuple<int[], string>(space, "Auxiliary"));
                            }
                        }
                    }
                    successfulNormal = true;
                }
            }
            // THis gives us reward rooms

            if(!successfulLarge && !successfulNormal)
            {
                if (width > minRewardDim.x && height > minRewardDim.y)
                {
                    // Randomly choose which split we prefer
                    if (UnityEngine.Random.Range(0, 100) < 50)
                    {
                        if (height >= minRewardDim.y * 2)
                        {
                            SplitHorizontal(space);
                        }
                        else if (width >= minRewardDim.x * 2)
                        {
                            SplitVertical(space);
                        }
                        else if(UnityEngine.Random.Range(0, 100) < rewardPercent)
                        {
                            roomsList.Enqueue(new Tuple<int[], string>(space, "Reward"));
                        }
                    }
                    else
                    {
                        if (width >= minRewardDim.x * 2)
                        {
                            SplitVertical(space);
                        }
                        else if (height >= minRewardDim.y * 2)
                        {
                            SplitHorizontal(space);
                        }
                        else if(UnityEngine.Random.Range(0, 100) < rewardPercent)
                        {
                            roomsList.Enqueue(new Tuple<int[], string>(space, "Reward"));
                        }
                    }
                }
            }
            count++;
        }

        // This is where we create the rooms
        int tempCount = 0;
        Debug.Log("Room Count = " + roomsList.Count);
        while (roomsList.Count > 0)
        {
            (int[] room, string roomType) = roomsList.Dequeue();

            int x1 = (int)room[0];
            int y1 = (int)room[1];
            int x2 = (int)room[2];
            int y2 = (int)room[3];

            int length = (x2 - 1) - (x1 + 1);
            int width = (y2 - 1) - (y1 + 1);

            GameObject tempRoom = new GameObject(tempCount.ToString());
            tempRoom.AddComponent<RoomNode>();

            RoomNode NewRoom = tempRoom.GetComponent<RoomNode>();
            int roomIndex = -1;
            string[,] tempString = null;
            if(roomType == "Normal")
            {
                roomIndex = UnityEngine.Random.Range(0, NormalRooms.RoomList.Count);
                //Debug.Log("RoomList count = " + NormalRooms.RoomList.Count);
                //Debug.Log("Room index = " + roomIndex);
                tempString = (string[,])NormalRooms.RoomList[roomIndex];
                //Debug.Log("Temp string = " + tempString);
                // Amount of arrays
                var roomLength = tempString.GetLength(0);
                // Array length
                var roomWidth = tempString.GetLength(1);
                Debug.Log("Allocated length: " + length);
                Debug.Log("Allocated width: " + width);
                Debug.Log("Normal room length: " + roomLength);
                Debug.Log("Normal room width: " + roomWidth);
                NewRoom.AddDimensions(roomLength, roomWidth);
            }
            else if(roomType == "Reward")
            {
                roomIndex = UnityEngine.Random.Range(0, RewardRooms.RoomList.Count);
                //Debug.Log("RoomList count = " + NormalRooms.RoomList.Count);
                //Debug.Log("Room index = " + roomIndex);
                tempString = (string[,])RewardRooms.RoomList[roomIndex];
                //Debug.Log("Temp string = " + tempString);
                // Amount of arrays
                var roomLength = tempString.GetLength(0);
                // Array length
                var roomWidth = tempString.GetLength(1);
                Debug.Log("Allocated length: " + length);
                Debug.Log("Allocated width: " + width);
                Debug.Log("Normal room length: " + roomLength);
                Debug.Log("Normal room width: " + roomWidth);
                NewRoom.AddDimensions(roomLength, roomWidth);
            }
            else
            {
                NewRoom.AddDimensions(length, width);
            }
            
            if (!HasEntry && roomType != "Reward")
            {
                NewRoom.RoomType = "Start";
                NewRoom.MaxNeighbors = 1;
                HasEntry = true;
                StartRoom = NewRoom;
            }
            /*else if (!HasBoss && roomsList.Count == 0)
            {
                NewRoom.RoomType = "Boss";
                NewRoom.MaxNeighbors = 2;
                HasBoss = true;
            }*/
            else
            {
                NewRoom.RoomType = roomType;
                NewRoom.MaxNeighbors = 3;
            }

            
            // Here we fill the negative space with empty space 
            // I.e. room creation
            if(roomType == "Normal" || roomType == "Reward")
            {
                //Debug.Log("Before 2f");
                for(int i = 0; i < NewRoom.length; i++)
                {
                    for (int j = 0; j < NewRoom.width; j++)
                    {
                        // Here is where we'd want to randomly choose from a static list
                        if(int.TryParse(tempString[i, j], out int result))
                        {   
                            //Debug.Log("Int result = " + result);
                            map[x1 + 1 + i, y1 + j + 1].value = result;
                            if(result == 1)
                            {
                                map[x1 + 1 + i, y1 +j + 1].room = NewRoom;
                                roomTiles.Add(map[x1 + 1 + i, y1 + j + 1]);
                            }
                            NewRoom.tileList[i, j] = map[x1 + 1 + i, y1 + j + 1];
                            NewRoom.tileCount++;
                        }
                        else
                        {
                            //Debug.Log("String result = " + result);
                            map[x1 + 1 + i, y1 + j + 1].value = 1;
                            map[x1 + 1 + i, y1 + j + 1].isObstacle = true;
                            map[x1 + 1 + i, y1 + j + 1].room = NewRoom;
                            roomTiles.Add(map[x1 + 1 + i, y1 + j + 1]);
                            NewRoom.tileList[i, j] = map[x1 + 1 + i, y1 + j + 1];
                            NewRoom.tileCount++;
                            ObstacleLookUp.SpawnObstacle(tempString[i, j], x1 + 1 + i, y1 + j + 1, NewRoom);
                        }
                        
                    }
                }
                //Debug.Log("Before Add Lights");
                AddLights(x1, y1, x2, y2, NewRoom);
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        map[x1 + 1 + i, y1 + 1 + j].value = 1;
                        map[x1 + 1 + i, y1 + 1 + j].room = NewRoom;
                        roomTiles.Add(map[x1 + 1 + i, y1 + 1 + j]);
                        NewRoom.tileList[i, j] = map[x1 + 1 + i, y1 + 1 + j];
                        NewRoom.tileCount++;
                    }
                }
                AddLights(x1, y1, x2, y2, NewRoom);
            }
            //Debug.Log("Before Calculate Center");
            NewRoom.CalculateCenter();
            //Debug.Log("After Calculate Center");
            if(NewRoom.RoomType == "Start")
            {
                SpawnPlayer(NewRoom);
            }

            Rooms.Add(NewRoom);
            tempCount++;
        }
        Debug.Log("After Room Creation");
        SortRooms();
        Debug.Log("Before Corridor creation");
        AddCorridors();
        Debug.Log("After corrider creation");
        AddEndRoom();
        AddEntryColliders();
        AddSpawners();
        for(int i = 0; i < 1; i++)
            AddObstacles();
       // AddWalls();
    }
    void AddWalls()
    {
        GameObject wallParent = new GameObject("WallParent");
        wallParent.transform.parent = transform;
        List<Vector3Int> possibleDoorVerticalPosition = new List<Vector3Int>();
        List<Vector3Int> possibleDoorHorizontalPosition = new List<Vector3Int>();
        List<Vector3Int> possibleWallHorizontalPosition = new List<Vector3Int>();
        List<Vector3Int> possibleWallVerticalPosition = new List<Vector3Int>();

        foreach(RoomNode room in Rooms)
        {
           PopulateWallLists(possibleDoorVerticalPosition, possibleDoorHorizontalPosition,
                             possibleWallHorizontalPosition, possibleWallVerticalPosition, room);
        }

         foreach(CorridorNode corr in Corridors)
        {
           PopulateWallLists(possibleDoorVerticalPosition, possibleDoorHorizontalPosition,
                             possibleWallHorizontalPosition, possibleWallVerticalPosition, corr);
        }

       

         foreach (var wallPosition in possibleWallHorizontalPosition)
        {
            CreateWall(wallParent, wallPosition, wallHorizontal);
        }
        foreach (var wallPosition in possibleWallVerticalPosition)
        {
            CreateWall(wallParent, wallPosition, wallVertical);
        }

    }

    //Function to add Corridor wall positions to list
    void  PopulateWallLists(List<Vector3Int> doorVertPos, List<Vector3Int> doorHorPos,
                                 List<Vector3Int> wallHorPos, List<Vector3Int> wallVertPos, CorridorNode corr)
        {
            



        }

    //function to add Room wall positions to list
    void  PopulateWallLists(List<Vector3Int> doorVertPos, List<Vector3Int> doorHorPos,
                                 List<Vector3Int> wallHorPos, List<Vector3Int> wallVertPos, RoomNode room) {
        for (int row = (int)room.bottomLeftCorner.x; row < (int)room.bottomRightCorner.x; row++)
        {
            //uncomment lines + switch .y to .z to enable third dimension

           // var wallPosition = new Vector3(row, 0, room.bottomLeftCorner.y);
           var wallPosition = new Vector3(row, 1f, room.bottomLeftCorner.y);
            AddWallPositionToList(wallPosition, wallHorPos, doorHorPos);
        }
        for (int row = (int)room.topLeftCorner.x; row < (int)room.topRightCorner.x; row++)
        {
            //var wallPosition = new Vector3(row, 1f, room.topRightCorner.y);
            var wallPosition = new Vector3(row, 1f, room.topRightCorner.y);
            AddWallPositionToList(wallPosition, wallHorPos, doorHorPos);
        }
        for (int col = (int)room.bottomLeftCorner.y; col < (int)room.topLeftCorner.y; col++)
        {
            //var wallPosition = new Vector3(room.bottomLeftCorner.x, 1f, col);
            var wallPosition = new Vector3(room.bottomLeftCorner.x,1f, col);
            AddWallPositionToList(wallPosition, wallVertPos, doorVertPos);
        }
        for (int col = (int)room.bottomRightCorner.y; col < (int)room.topRightCorner.y; col++)
        {
            //var wallPosition = new Vector3(room.bottomRightCorner.x, 1f, col);
            var wallPosition = new Vector3(room.bottomRightCorner.x,1f, col);
            AddWallPositionToList(wallPosition, wallVertPos, doorVertPos);
        }
    }

    private void AddWallPositionToList(Vector3 wallPosition, List<Vector3Int> wallList, List<Vector3Int> doorList)
    {
        Vector3Int point = Vector3Int.CeilToInt(wallPosition);
        if (wallList.Contains(point)){
            doorList.Add(point);
            wallList.Remove(point);
        }
        else
        {
            wallList.Add(point);
        }
    }

    private void CreateWall(GameObject wallParent, Vector3Int wallPosition, GameObject Wall)
    {
        Instantiate(Wall, wallPosition, Quaternion.identity, wallParent.transform);
    }
    void AddObstacles()
    {
        foreach(RoomNode room in Rooms)
        {
            if (room.RoomType == "Start" || 
                room.RoomType == "Shop" || 
                room.RoomType == "Boss" ||
                room.RoomType == "Normal" ||
                room.RoomType == "Key" ||
                room.RoomType == "Door" ||
                room.RoomType == "Auxiliary" ||
                room.RoomType == "Reward")
            {
                continue;
            }
            //ObstacleInjector.PlaceObstacles(room);
            ObstacleInjector.PlacePillars(room);
        }
    }

    void AddSpawners()
    {
        foreach(RoomNode room in Rooms)
        {
            Vector3 pos1 = new Vector3(room.roomCenter.x , 0, room.roomCenter.y);
            if (room.RoomType == "Start")
            {
                // Instantiate(ShopKeeper, pos1, Quaternion.identity);
                continue;
            }
            pos1 = new Vector3(room.roomCenter.x, 0, room.roomCenter.y);
            GameObject spawnedObject;
            if(room.RoomType == "Boss")
            {
                spawnedObject = Instantiate(BossSpawner, pos1, Quaternion.identity);
            }
            else if(room.RoomType == "Shop")
            {
                spawnedObject = Instantiate(ShopKeeperSpawner, pos1, Quaternion.identity);
                //spawnedObject.transform.parent = room.transform;
                //ShopKeeper = spawnedObject.transform.Find("ShopKeeper(clone)").gameObject;
                //ShopKeeper.name = "ShopKeeper";
            }
            else if(room.RoomType == "Reward")
            {
                // Not implemented yet
                continue;
            }
            else if(room.RoomType == "Auxiliary")
            {
                spawnedObject = Instantiate(PuzzleSpawner, pos1, Quaternion.identity);
                //GameObject lk = Instantiate(PuzzleSpawner, pos1, Quaternion.identity);
                //lk.transform.parent = room.transform;
            }
            else if(room.RoomType == "Large")
            {
                spawnedObject = Instantiate(Spawner[Random.Range(0, Spawner.Count - 2)], pos1, Quaternion.identity);
            }
            else
            {

                spawnedObject = Instantiate(Spawner[Random.Range(0, Spawner.Count - 2)], pos1, Quaternion.identity);

            }
            spawnedObject.transform.parent = room.transform;

            room.gameObject.AddComponent<RoomClearCheck>();
        }
    }

    void AddEntryColliders()
    {
        foreach(RoomNode room in Rooms) {
             if(room.RoomType != "Start")
             {
                Vector3 pos1 = new Vector3(room.roomCenter.x + 1f, 0, room.roomCenter.y + 1f);
                GameObject entryCollider1 = Instantiate(EntryCollider, pos1, Quaternion.identity);
                entryCollider1.transform.parent = room.transform;
                entryCollider1.GetComponent<EntryCollider>().tilemap = this.AutoTiler.GetTilemap();
            }
        }
        foreach(CorridorNode corridor in Corridors)
        {
            if (corridor != null && corridor.tileList.Count > 0)
            {
                roomTiles.AddRange(corridor.tileList);
                // Debug.Log("Tilelist count: " + corridor.tileList.Count);
                // GRab 1st element from tilelist and roomlist
                TileNode tile1 = corridor.tileList[0];
                RoomNode room1 = corridor.TargetRoomList[0];
                
                if(room1.RoomType != "Start")
                {
                    // // instantiate entry collider at 1st tile coupled with 1st room
                    // Vector3 pos1 = new Vector3(tile1.x, tile1.y, 0);
                    // GameObject entryCollider1 = Instantiate(EntryCollider, pos1, Quaternion.identity);
                    // entryCollider1.transform.parent = room1.transform;
                }

                //grab 2nd element
                TileNode tile2 = corridor.tileList.Last();
                RoomNode room2 = corridor.TargetRoomList.Last();

                // instantiate entry collider at 2nd tile coupled with 2nd room
                // Vector3 pos2 = new Vector3(tile2.x, tile2.y, 0);
                // GameObject entryCollider2 = Instantiate(EntryCollider, pos2, Quaternion.identity);
                // entryCollider2.transform.parent = room2.transform;
            }
            
        }
    }

    void SpawnPlayer(RoomNode SpawnRoom)
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        Vector3 spawnPosition = new Vector3(SpawnRoom.roomCenter.x, 1.2f, SpawnRoom.roomCenter.y);
        Player.transform.position = spawnPosition;
        Player.GetComponent<Player>().setSpawnPoint(spawnPosition);
         if(PlayerProgressManager.hasData) {
            Debug.Log("Loading player data");
            PlayerProgressManager.LoadPlayer(Player.GetComponentInChildren<PlayerWeapon>().gameObject, Player.gameObject);
           // Player.GetComponent<Player>() = PlayerProgressManager.currentPlayerState;
           // Player.GetComponentInChildren<PlayerWeapon>() = PlayerProgressManager.currentPlayerWeaponState;
             }
       // controls.SetPosition(Player.transform.position);
        Vector3 weaponSpawn = new Vector3(spawnPosition.x - 3, spawnPosition.y, spawnPosition.z);
        GameObject spawnWeapon = Instantiate(SpawnWeapon, weaponSpawn, Quaternion.identity);
    }

    /* RoomNode CreateRoom(int x1, int y1, int x2, int y2)
    {
        RoomNode New = new RoomNode("Normal");

        int area = (x2 - x1) * (y2 - y1);
        // if (area)

        return New;
    }*/

    // This function sorts all Rooms according to their distance from eachother
    // This helps with optimizing corridor creation
    void SortRooms()
    {
        // Go through every room
        foreach(RoomNode room in Rooms)
        {
            // Go through all other rooms
            foreach(RoomNode neighbor in Rooms)
            {
                // Not including itself
                if (neighbor == room)
                {
                    continue;
                }

                SortByDistance(room, neighbor);
            }
        }
        /*
            if((room.RoomType) == "Start")
            {
                RoomNode bossRoom = room.RoomsByDistance[room.RoomsByDistance.Count-1];
                bossRoom.RoomType = "Boss";
                BossRoom = bossRoom;
            }
        }
        int index = Random.Range(1, 4);
        if(index >= BossRoom.RoomsByDistance.Count)
        {
            index = 1;
        }
        RoomNode shop = BossRoom.RoomsByDistance[index];
        if(shop == StartRoom)
        {
            shop = BossRoom.RoomsByDistance[index-1];
        }
        shop.RoomType = "Shop";
        ShopRoom = shop;*/
    }

    // This function goes through current list of rooms and inserts room in question accordingly
    void SortByDistance(RoomNode room, RoomNode neighbor)
    {
        for(int i = 0; i < room.RoomsByDistance.Count; i++)
        {
            var check = room.RoomsByDistance[i];
            var distance1 = Vector2.Distance(room.roomCenter, check.roomCenter);
            var distance2 = Vector2.Distance(room.roomCenter, neighbor.roomCenter);
            if (distance2 < distance1)
            {
                room.RoomsByDistance.Insert(i, neighbor);
                return;
            }
        }
        // If neighbor is furthest away out of all compared so far add it to back of list
        room.RoomsByDistance.Add(neighbor);
    }

    void AddEndRoom()
    {
        int distance = 0;
        RoomNode end = new RoomNode();
        foreach(RoomNode room in Rooms)
        {
            if(room.DistanceFromStart > distance && room.RoomType != "Reward")
            {
                distance = room.DistanceFromStart;
                end = room;
                //boss.RoomType = "Boss";
            }
        }
        EndRoom = end;
        if(hasBoss)
        {
            EndRoom.RoomType = "Boss";
        }
        else
        {
            EndRoom.RoomType = "Key";
        }
        /*Debug.Log("Boss distance: " + BossRoom.DistanceFromStart);
        int r = Random.Range(1, 4);
        if(r >= BossRoom.RoomsByDistance.Count)
        {
            r = 1;
        }
        int distanceFromBoss = BossRoom.DistanceFromStart - r;
        Debug.Log("Shop's distance: " + distanceFromBoss);
        foreach(RoomNode room in Rooms)
        {
            if(room.DistanceFromStart == distanceFromBoss)
            {
                ShopRoom = room;
                break;
            }
        }

        
        //RoomNode shop = BossRoom.RoomsByDistance[distanceFromBoss];
        if(ShopRoom == StartRoom)
        {
            ShopRoom = BossRoom.RoomsByDistance[r-1];
        }
        ShopRoom.RoomType = "Shop";
        //ShopRoom = shop;*/

        //---------------- SHOP ----------------
        int shopIndex = UnityEngine.Random.Range(1, 4);
        if(shopIndex >= EndRoom.RoomsByDistance.Count)
        {
            shopIndex = 1;
        }
        RoomNode shop = EndRoom.RoomsByDistance[shopIndex];
        if(shop == StartRoom)
        {
            shop = EndRoom.RoomsByDistance[shopIndex-1];
        }
        shop.RoomType = "Shop";
        ShopRoom = shop;

        //---------------- DOOR ----------------
        if(!hasBoss)
        {
            int doorIndex = UnityEngine.Random.Range(roomsList.Count / 3, roomsList.Count / 2);
            if(doorIndex == shopIndex)
            {
                doorIndex = 1;
            }
            RoomNode door = StartRoom.RoomsByDistance[doorIndex];
            if(door == StartRoom)
            {
                door = StartRoom.RoomsByDistance[doorIndex-1];
            }
            door.RoomType = "Door";
            DoorRoom = door;
        }
    }

    void AddCorridors()
    {
        /* List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (RoomNode Room in Rooms)
        {
            roomCenters.Add(Room.roomCenter);
        }

        var currentCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentCenter);

        int i = 0;
        while(roomCenters.Count > 0)
        {
            if (i > 100)
            {
                Debug.Log("While loop timed out");
                return; 
            }
            Vector2Int closest = FindClosestPoint(currentCenter, roomCenters);
            roomCenters.Remove(closest);
            CreateCorridor(currentCenter, closest);

            currentCenter = closest;
            i++;
        }*/

        foreach (RoomNode room in Rooms)
        {
            foreach (RoomNode neighbor in room.RoomsByDistance)
            {
                ConnectRooms(room, neighbor);
            }
        }
        AddDistanceFromStart();
    }

    public void AddDistanceFromStart()
    {
        StartRoom.DistanceFromStart = 0;
        Queue<RoomNode> roomsToSearch = new Queue<RoomNode>();
        roomsToSearch.Enqueue(StartRoom);
        //Debug.Log("In Distance From Start");
        while(roomsToSearch.Count > 0)
        {
            RoomNode currentRoom = roomsToSearch.Dequeue();
            //Debug.Log("Current Room Distance From Start: " + currentRoom.DistanceFromStart);
            //Debug.Log("Current Room Neighbor Count: " + currentRoom.NeighborRooms.Count);
            foreach(RoomNode room in currentRoom.NeighborRooms)
            {
                //Debug.Log("In For Loop");
                if(room.DistanceFromStart <= 0 && room != StartRoom)
                {
                    room.DistanceFromStart = currentRoom.DistanceFromStart + 1;
                    roomsToSearch.Enqueue(room);
                }
            }
            //Debug.Log("Rooms To Search Count: " + roomsToSearch.Count);
        }
    }

    bool RoomsIncompatible(RoomNode room, RoomNode neighbor)
    {
        // Checks if the two rooms are the start and a reward room
        if(room.RoomType == "Start" || room.RoomType == "Reward")
        {
            if(neighbor.RoomType == "Start" || neighbor.RoomType == "Reward")
            {
                return true;
            }
        }
        return false;
    }
    void ConnectRooms(RoomNode room, RoomNode neighbor)
    {
        if(RoomsIncompatible(room, neighbor))
        {
            return;
        }
        if (room.NeighborCount >= room.MaxNeighbors)
        {
            return;
        }
        if (neighbor.NeighborCount >= neighbor.MaxNeighbors)
        {
            return;
        }
        //CorridorNode corridor = CreateCorridor(room, neighbor);
        CorridorNode corridor = Helper.CreateCorridor(room, neighbor, room.CenterTile, neighbor.CenterTile, ref map);
        if (corridor == null)
        {
            corridor = Helper.CreatePassage(room, neighbor, room.CenterTile, neighbor.CenterTile, ref map);
        }
        if(corridor == null)
        {
            return;
        }

        /*if(IsBorderingRoom(corridor))
        {
            Debug.Log("Corridor is bordering room");
            NullifyCorridor(corridor);
            corridor = null;
            return;
        }*/

        Corridors.Add(corridor);
        RoomNode.ConnectRooms(room, neighbor);

    }

    private CorridorNode CreateCorridor(RoomNode room, RoomNode neighbor)
    {
        Vector2Int currentRoomCenter = room.roomCenter;
        Vector2Int destination = neighbor.roomCenter;
        var position = currentRoomCenter;
        CorridorNode corridor = new CorridorNode();
        // Here we randomly choose a directional preference
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            // Moving vertically
            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else
                {
                    position += Vector2Int.down;
                }
                if (map[position.x, position.y].value == 0 && map[position.x-1, position.y].value == 0)
                {
                    map[position.x, position.y].value = 2;
                    map[position.x-1, position.y].value = 2;
                    corridor.tileList.Add(map[position.x, position.y]);
                    corridor.tileList.Add(map[position.x-1, position.y]);
                }
                else if (map[position.x, position.y].value == 1 || map[position.x-1, position.y].value == 1)
                {
                    if ((map[position.x, position.y].room != room && map[position.x, position.y].room != neighbor) || (map[position.x-1, position.y].room != room && map[position.x-1, position.y].room != neighbor))
                    {
                        corridor.NullifyCorridor();
                        corridor = null;
                        return null;
                    }
                }
                /*Debug.Log("Before InMapRange check");
                if(Helper.IsInMapRange(position.x-1, position.y, ref map) && Helper.IsInMapRange(position.x, position.y, ref map))
                {
                    AddCorridorTile(corridor, map[position.x, position.y], map[position.x-1, position.y]);
                }*/
                
            }
            // Moving horizontally
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }
                else
                {
                    position += Vector2Int.left;
                }
                if (map[position.x, position.y].value == 0 && map[position.x, position.y-1].value == 0)
                {
                    map[position.x, position.y].value = 2;
                    map[position.x, position.y-1].value = 2;
                    corridor.tileList.Add(map[position.x, position.y]);
                    corridor.tileList.Add(map[position.x, position.y-1]);
                }
                else if (map[position.x, position.y].value == 1 || map[position.x, position.y-1].value == 1)
                {
                    if ((map[position.x, position.y].room != room && map[position.x, position.y].room != neighbor) || (map[position.x, position.y-1].room != room && map[position.x, position.y-1].room != neighbor))
                    {
                        corridor.NullifyCorridor();
                        corridor = null;
                        return null;
                    }
                }
            }
        }
        else
        {
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }
                else
                {
                    position += Vector2Int.left;
                }
                if (map[position.x, position.y].value == 0 && map[position.x, position.y-1].value == 0)
                {
                    map[position.x, position.y].value = 2;
                    map[position.x, position.y-1].value = 2;
                    corridor.tileList.Add(map[position.x, position.y]);
                    corridor.tileList.Add(map[position.x, position.y-1]);
                }
                else if (map[position.x, position.y].value == 1 || map[position.x, position.y-1].value == 1)
                {
                    if ((map[position.x, position.y].room != room && map[position.x, position.y].room != neighbor) || (map[position.x, position.y-1].room != room && map[position.x, position.y-1].room != neighbor))
                    {
                        corridor.NullifyCorridor();
                        corridor = null;
                        return null;
                    }
                }
            }
            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else
                {
                    position += Vector2Int.down;
                }
                if (map[position.x, position.y].value == 0 && map[position.x-1, position.y].value == 0)
                {
                    map[position.x, position.y].value = 2;
                    map[position.x-1, position.y].value = 2;
                    corridor.tileList.Add(map[position.x, position.y]);
                    corridor.tileList.Add(map[position.x-1, position.y]);
                }
                else if (map[position.x, position.y].value == 1 || map[position.x-1, position.y].value == 1)
                {
                    if ((map[position.x, position.y].room != room && map[position.x, position.y].room != neighbor) || (map[position.x-1, position.y].room != room && map[position.x-1, position.y].room != neighbor))
                    {
                        corridor.NullifyCorridor();
                        corridor = null;
                        return null;
                    }
                }
            }
        }

        corridor.TargetRoomList.Add(room);
        corridor.TargetRoomList.Add(neighbor);

        if(corridor.IsBorderingRoom())
        {
            corridor.NullifyCorridor();
            return null;
        }

        return corridor;
    }

    private void AddCorridorTile(CorridorNode corridor, TileNode tile1, TileNode tile2)
    {
        if (tile1.value == 0 && tile1.value == 0)
        {
            tile1.value = 2;
            tile2.value = 2;
            corridor.tileList.Add(tile1);
            corridor.tileList.Add(tile2);
        }
        else if (tile1.value == 1 || tile2.value == 1)
        {
            if ((tile1.room != corridor.TargetRoomList[0] && tile1.room != corridor.TargetRoomList[1]) || 
                (tile2.room != corridor.TargetRoomList[0] && tile2.room != corridor.TargetRoomList[1]))
            {
                NullifyCorridor(corridor);
                corridor = null;
            }
        }
    }

    private void NullifyCorridor(CorridorNode corridor)
    {
        foreach(TileNode tile in corridor.tileList)
        {
            map[tile.x, tile.y].value = 0;
        }
    }

    private Vector2Int FindClosestPoint(Vector2Int currentCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    void AddLights(int x1, int y1, int x2, int y2, RoomNode room)
    {
        var lightPrefab1 = Instantiate(LightPrefab, new Vector3(x1 + 8, 0, y1 + 8), Quaternion.identity);
        var lightPrefab2 = Instantiate(LightPrefab, new Vector3(x1 + 8, 0, y2 - 8), Quaternion.identity);
        var lightPrefab3 = Instantiate(LightPrefab, new Vector3(x2 - 8, 0, y1 + 8), Quaternion.identity);
        var lightPrefab4 = Instantiate(LightPrefab, new Vector3(x2 - 8, 0, y2 - 8), Quaternion.identity);

        lightPrefab1.transform.parent = room.gameObject.transform;
        lightPrefab2.transform.parent = room.gameObject.transform;
        lightPrefab3.transform.parent = room.gameObject.transform;
        lightPrefab4.transform.parent = room.gameObject.transform;
    }

    // Splits map horizontally
    void SplitHorizontal(int[] roomSpace)
    {
        var x1 = roomSpace[0];
        var y1 = roomSpace[1];
        var x2 = roomSpace[2];
        var y2 = roomSpace[3];

        var buffer = (int)(y2 - y1)/3;
        var ySplit = UnityEngine.Random.Range(y1 + 1 + buffer, y2 - buffer);
        var room1 = new int[] { x1, y1, x2, ySplit };
        var room2 = new int[] { x1, ySplit + 1, x2, y2 };

        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            queue.Add(room1);
            queue.Add(room2);
        }
        else
        {
            queue.Add(room2);
            queue.Add(room1);
        }
    }

    // Splits map vertically
    void SplitVertical(int[] roomSpace)
    {
        var x1 = roomSpace[0];
        var y1 = roomSpace[1];
        var x2 = roomSpace[2];
        var y2 = roomSpace[3];

        var buffer = (int)(x2 - x1)/3;
        var xSplit = UnityEngine.Random.Range(x1 + 1 + buffer, x2 - buffer);
        var room1 = new int[] { x1, y1, xSplit, y2 };
        var room2 = new int[] { xSplit + 1, y1, x2, y2 };

        // print("In vertical.");
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            queue.Add(room1);
            queue.Add(room2);
        }
        else
        {
            queue.Add(room2);
            queue.Add(room1);
        }

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
                if (map[x, y].value == 0 || map[x, y].isObstacle)
                {
                    Gizmos.color = new Color(0, 0, 0, 1f);
                }
                else if (map[x, y].value == 1)
                {
                    if (map[x, y].room.RoomType == "Start")
                    {
                        Gizmos.color = new Color(0, 255, 0, 1f);
                    }
                    else if (map[x, y].room.RoomType == "Boss" || map[x, y].room.RoomType == "Key")
                    {
                        Gizmos.color = new Color(255, 0, 0, 1f);
                    }
                    else if (map[x, y].room.RoomType == "Door")
                    {
                        Gizmos.color = new Color(155/255f, 0, 0, 1f);
                    }
                    else if (map[x, y].room.RoomType == "Shop")
                    {
                        Gizmos.color = new Color(210/255f, 105/255f, 30/255f, 1f);
                    }
                    else if(map[x, y].room.RoomType == "Reward")
                    {
                        Gizmos.color = new Color(0, 0, 255, 1f);
                    }
                    else if(map[x, y].room.RoomType == "Auxiliary")
                    {
                        Gizmos.color = new Color(130/255f, 115/255f, 150/255f, 1f);
                    }
                    else
                    {
                        Gizmos.color = new Color(255, 255, 0, 1f);
                    }
                    
                }
                else
                {
                    Gizmos.color = new Color(0, 255, 255, 1f);
                }

                Gizmos.DrawCube(new Vector3(x, 0, y), new Vector3(1, 1, 1));
            }
        }
    }
}
