using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// This state is responsible for player movement post-Dive state
// Maybe there's an upgrade that foregoes this state entirely???
// Press Space to transition from Prone to RunGun
public class PlayerProneState : PlayerBaseState
{
    private Camera mainCamera;

    public bool standing = true;
    private float standTime;
    public PlayerInput playerInput;
    public AgentAnimations playerAnimations;
    private bool fireButtonDown = false;

    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered Prone State");
        // Debug.Log("Standing = " + standing);
        TimeManager.RevertSlowMotion();
        mainCamera = Camera.main;
        standing = false;
        // Debug.Log("Standing = " + standing);
        playerInput = Player.playerInput;
        playerInput.PlayerMovement.ResetSpeed();
        standTime = playerInput.PlayerMovement.MovementData.standingDelay;

    }

    public override void UpdateState(PlayerStateManager Player)
    {
        GetStandInput();
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetReloadInput();
        CalculateStandTime();
        if (standing == true && standTime <= 0)
        {
             Player.GetComponentInChildren<AgentAnimations>().SetStandAnimation();
            Player.SwitchState(Player.RunGunState);
        }
    }
    
    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (fireButtonDown == false)
            {
                fireButtonDown = true;
                playerInput.OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (fireButtonDown == true)
            {
                fireButtonDown = false;
                playerInput.OnFireButtonReleased?.Invoke();
            }
        }
    }

    private void GetReloadInput()
    {
        if (Input.GetAxisRaw("Reload") > 0)
        {
            playerInput.OnReloadButtonPressed?.Invoke();
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // This invokes AgentRenderer.FaceDirection and PlayerWeapon.AimWeapon
        playerInput.OnPointerPositionChange?.Invoke(mouseInWorldSpace);
    }

    private void GetMovementInput()
    {
        playerInput.OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
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
