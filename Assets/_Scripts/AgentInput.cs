using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This script is responsible for receiving input from player
public class AgentInput : MonoBehaviour, IAgentInput
{
    private Camera mainCamera;

    private bool fireButtonDown = false;

    // The Vector2 corresponds to the magnitude of movement in the (x, y)    wasd
    // (0, 0), (0, 1), (1, 0), (1, 1), (0, -1), (-1, 0), (-1, -1), (1, -1), (-1, 1)
    // Passes the Vector2 to AgentMovement.MoveAgent hence SerializeField
    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    // Vector2 coresponds to the position of the mouse on the screen
    // This funciton is used to aim the weapon and change player direction
    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    // Calls PlayerWeapon.shoot
    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    // Calls PlayerWeapon.StopShooting
    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    // Calls PlayerWeapon.StopShooting
    [field: SerializeField]
    public UnityEvent OnReloadButtonPressed { get; set; }

    // Calls SceneManager.RestartScene
    [field: SerializeField]
    public UnityEvent OnRestartButtonPressed { get; set; }

    // Calls SceneManager.RestartScene
    [field: SerializeField]
    public UnityEvent OnRespawnButtonPressed { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetReloadInput();
        // GetRestartInput();
        GetRespawnInput();
    }

    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (fireButtonDown == false)
            {
                fireButtonDown = true;
                OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if (fireButtonDown == true)
            {
                fireButtonDown = false;
                OnFireButtonReleased?.Invoke();
            }
        }
    }
     
    private void GetReloadInput()
    {
        if (Input.GetAxisRaw("Reload") > 0)
        {
            OnReloadButtonPressed?.Invoke();
        }
    }

    private void GetRestartInput()
    {
        if (Input.GetAxisRaw("Space") > 0)
        {
            // This will restart the entire scene
            OnRestartButtonPressed?.Invoke();
        }
    }

    private void GetRespawnInput()
    {
        if (Input.GetAxisRaw("Space") > 0)
        {
            Debug.Log("In Respawn Input");
            // This will respawn the player to their original position at start of scene
            OnRespawnButtonPressed?.Invoke();
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // This invokes AgentRenderer.FaceDirection and PlayerWeapon.AimWeapon
        OnPointerPositionChange?.Invoke(mouseInWorldSpace);
    }


    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
