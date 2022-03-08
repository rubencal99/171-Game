using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObstacleInjector
{
    public static void PlaceObstacles(RoomNode room)
    {
        int obstacleOdds = Random.Range(0, 100);
        if(obstacleOdds < 30)
        {
            ObstacleInjector.PlacePit(room);
        }
        else if(obstacleOdds < 50)
        {
            ObstacleInjector.PlaceCornerCrosses(room);
        }
        else if(obstacleOdds < 70)
        {
            ObstacleInjector.PlaceCenterCross(room);
        }

        else if(obstacleOdds < 80)
        {
            ObstacleInjector.PlaceCenterX(room);
        }
        // Corner Squares is buggy
        else if(obstacleOdds < 90)
        {
            ObstacleInjector.PlaceCornerSquares(room);
        }
        
    }

    static void PlaceCenterX(RoomNode room)
    {
        int centerX = (int)(room.length / 2);
        int centerY = (int)(room.width / 2);
        
        ObstacleInjector.PlaceLargeX(room, centerX, centerY);
    }

    static void PlaceCenterCross(RoomNode room)
    {
        int centerX = (int)(room.length / 2);
        int centerY = (int)(room.width / 2);
        
        ObstacleInjector.PlaceLargeCross(room, centerX, centerY);
    }

    static void PlaceCornerSquares(RoomNode room)
    {
        int quarterX = (int)(room.length / 4);
        int quarterY = (int)(room.width / 4);

        int centerX = (int)(room.length / 2);
        int centerY = (int)(room.width / 2);
        
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if((j != 0 && i != 0)) //|| (j == 0 && i == 0))
                {
                    ObstacleInjector.PlaceSmallSquare(room, centerX + i * quarterX, centerY + j * quarterY);
                }
                //room.tileList[centerX + i * quarterX, centerY + j * quarterY].value = 0;
            }
        }
    }

    static void PlaceCornerCrosses(RoomNode room)
    {
        int quarterX = (int)(room.length / 4);
        int quarterY = (int)(room.width / 4);

        int centerX = (int)(room.length / 2);
        int centerY = (int)(room.width / 2);
        
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if((j != 0 && i != 0)) //|| (j == 0 && i == 0))
                {
                    ObstacleInjector.PlaceSmallCross(room, centerX + i * quarterX, centerY + j * quarterY);
                }
                //room.tileList[centerX + i * quarterX, centerY + j * quarterY].value = 0;
            }
        }
    }

    static void PlaceLargeX(RoomNode room, int centerCrossX, int centerCrossY)
    {
        for(int i = -3; i <= 3; i++)
        {
            for(int j = -3; j <= 3; j++)
            {
                if(Mathf.Abs(i) == Mathf.Abs(j))
                {
                    room.tileList[centerCrossX + i, centerCrossY + j].value = 0;
                    room.tileList[centerCrossX + i, centerCrossY + j].isObstacle = true;
                }
            }
        }
    }

    static void PlaceLargeCross(RoomNode room, int centerCrossX, int centerCrossY)
    {
        for(int i = -2; i <= 2; i++)
        {
            for(int j = -2; j <= 2; j++)
            {
                if(i == 0 || j == 0)
                {
                    room.tileList[centerCrossX + i, centerCrossY + j].value = 0;
                    room.tileList[centerCrossX + i, centerCrossY + j].isObstacle = true;
                }
            }
        }
    }

    static void PlaceSmallSquare(RoomNode room, int centerSquareX, int centerSquareY)
    {
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                room.tileList[centerSquareX + i, centerSquareX + j].value = 0;
                  room.tileList[centerSquareX + i, centerSquareX + j].isObstacle = true;
            }
        }
    }

    static void PlaceSmallCross(RoomNode room, int centerCrossX, int centerCrossY)
    {
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if(i == 0 || j == 0)
                {
                    room.tileList[centerCrossX + i, centerCrossY + j].value = 0;
                    room.tileList[centerCrossX + i, centerCrossY + j].isObstacle = true;
                }
            }
        }
    }

    static void PlacePit(RoomNode room)
    {
        int x = Random.Range(1, 3);
        int y = Random.Range(1, 3);
        int centerX = (int)(room.length / 2);
        int centerY = (int)(room.width / 2);
        
        for(int i = -x; i <= x; i++)
        {
            for(int j = -y; j <= y; j++)
            {
                room.tileList[centerX + i, centerY + j].value = 0;
                room.tileList[centerX + i, centerY + j].isObstacle = true;
            }
        }
    }
}
