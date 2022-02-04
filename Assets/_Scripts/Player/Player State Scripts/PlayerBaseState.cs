using UnityEngine;

// This is the base state for our Player object
// What is our player doing? What can/can't he do while he's doing it?
// These questions are controlled by these states.
public abstract class PlayerBaseState
{
    // This function controls what happens when this state is activated
    public abstract void EnterState(PlayerStateManager Player);

    // This is basically Update(), called every frame
    public abstract void UpdateState(PlayerStateManager Player);
}
