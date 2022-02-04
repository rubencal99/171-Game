using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This state is responsible for when the player is Diving
// This state should only be active for a fraction of a second (unless we develop upgrades like slowmo bullet time while diving)
// This state automatically transitions to prone state
public class PlayerDiveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager Player)
    {
        Debug.Log("Entered Dive State");
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        
    }
}
