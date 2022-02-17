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
    private float standTime;
    public PlayerInput playerInput;

    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered Prone State");
        // Debug.Log("Standing = " + standing);
        standing = false;
        // Debug.Log("Standing = " + standing);
        playerInput = Player.playerInput;
        playerInput.PlayerMovement.ResetSpeed();
        standTime = playerInput.PlayerMovement.MovementData.standingDelay;

    }

    public override void UpdateState(PlayerStateManager Player)
    {
        GetStandInput();
        CalculateStandTime();
        if (standing == true && standTime <= 0)
        {
            Player.SwitchState(Player.RunGunState);
        }
    }

    private void CalculateStandTime()
    {
        if (standTime > 0)
        {
            standTime -= Time.deltaTime;
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
