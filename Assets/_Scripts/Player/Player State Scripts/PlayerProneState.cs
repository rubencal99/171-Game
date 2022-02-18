using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// This state is responsible for player movement post-Dive state
// Maybe there's an upgrade that foregoes this state entirely???
// Press Space to transition from Prone to RunGun
public class PlayerProneState : PlayerBaseState
{
    private bool standing;
    public PlayerInput playerInput;
    public AgentAnimations playerAnimations;

    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered Prone State");
        // Debug.Log("Standing = " + standing);
        standing = false;
        // Debug.Log("Standing = " + standing);
        playerInput = Player.playerInput;
        playerInput.PlayerMovement.ResetSpeed();
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        GetStandInput();
        if (standing == true)
        {
             Player.GetComponentInChildren<AgentAnimations>().SetStandAnimation();
            Player.SwitchState(Player.RunGunState);
        }
    }

    private void GetStandInput()
    {
        if (Input.GetAxisRaw("Space") > 0)
        {
            // Debug.Log("Space button pressed while Prone");
            standing = true;
            playerInput.OnStandButtonPressed?.Invoke();
        }
    }
}
