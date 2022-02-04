using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunGunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager Player)
    {
        Debug.Log("Entered RunGun State");
    }

    public override void UpdateState(PlayerStateManager Player)
    {

    }
}
