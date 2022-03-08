using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public static RoomTracker instance;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
