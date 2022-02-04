using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This state is responsible for player movement post-Dive state
// Maybe there's an upgrade that foregoes this state entirely???
// Press Space to transition from Prone to RunGun
public class PlayerProneState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager Player)
    {
        Debug.Log("Entered Prone State");
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        
    }
}
