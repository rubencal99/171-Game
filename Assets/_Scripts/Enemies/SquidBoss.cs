using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBoss : MonoBehaviour
{
    public static bool inCyclone = false;
    public static int cycloneAttempts = 3;
    public BossMovement bossMovement;

    public void Start()
    {
        inCyclone = false;
        bossMovement = transform.parent.GetComponent<BossMovement>();
    }

    public void AdjustCycloneSpeed(bool val)
    {
        if(val)
        {
            inCyclone = true;
        }
        else
        {
            inCyclone = false;
        }
    }
}
