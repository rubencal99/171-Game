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
    public PlayerInput playerInput;
    private bool fireButtonDown = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered Prone State");
        // Debug.Log("Standing = " + standing);
        TimeManager.RevertSlowMotion();
        standing = false;
        // Debug.Log("Standing = " + standing);
        playerInput = Player.playerInput;
        mainCamera = Camera.main;
        playerInput.PlayerMovement.ResetSpeed();
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        GetStandInput();
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetReloadInput();
        if (standing == true)
        {
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
}
