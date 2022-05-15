using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class RewardRooms
{
    public static ArrayList RoomList = new ArrayList();

    static RewardRooms()
    {
        // Basic Reward Room 15 x 15
        RoomList.Add(RoomInjector.Rotate(new string[,]{
            {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "t", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
        }));

        RoomList.Add(RoomInjector.Rotate(new string[,]{
            {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "lc", "lc", "lc", "lc", "lc", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "lc", "1", "1", "1", "lc", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "lc", "1", "t", "1", "lc", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "lc", "1", "1", "1", "lc", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "0"},
            {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"},
        }));
    }
}
