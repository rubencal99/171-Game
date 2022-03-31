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
    private bool throwButtonDown = false;
    private bool meleeButtonDown = false;
    private bool reloadButtonDown = false;

    public bool dodging = false; // bool to check if dodging
    
    [SerializeField]
    public float DodgeTimer;

    // The Vector2 corresponds to the magnitude of movement in the (x, y)    wasd
    // (0, 0), (0, 1), (1, 0), (1, 1), (0, -1), (-1, 0), (-1, -1), (1, -1), (-1, 1)
    // Passes the Vector2 to AgentMovement.MoveAgent hence SerializeField
    [field: SerializeField]
    public UnityEvent<Vector3> OnMovementKeyPressed { get; set; }

    // Vector2 coresponds to the position of the mouse on the screen
    // This funciton is used to aim the weapon and change player direction
    [field: SerializeField]
    public UnityEvent<Vector3> OnPointerPositionChange { get; set; }

    // Dodge Mechanic
    // Vector2 Corresponds towards the movement of where the dodge roll happens
    // *************************
    [field: SerializeField]
    public UnityEvent<Vector3> OnDodgeKeyPressed { get; set; }
    // *************************

    // Calls PlayerWeapon.shoot
    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    // Calls PlayerWeapon.StopShooting
    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    // Calls PlayerWeapon.tryReloading
    [field: SerializeField]
    public UnityEvent OnReloadButtonPressed { get; set; }

    // Calls Player.ThowItem
    [field: SerializeField]
    public UnityEvent OnThrowButtonPressed { get; set; }

    // Calls SceneManager.RestartScene
    [field: SerializeField]
    public UnityEvent OnRestartButtonPressed { get; set; }

    // Calls SceneManager.RestartScene
    [field: SerializeField]
    public UnityEvent OnRespawnButtonPressed { get; set; }

    // Calls PlayerWeapon.UseMelee
    [field: SerializeField]
    public UnityEvent OnMeleeButtonPressed { get; set; }

     [field: SerializeField]
    public UnityEvent OnStandButtonPressed { get; set; }


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetThrowInput();
        GetMeleeInput();
        GetReloadInput();
        // GetRestartInput();
        GetRespawnInput();
        // GetDodgeInput();
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
             Debug.Log("In throw");
             if (reloadButtonDown == false) {
                reloadButtonDown = true;
                OnReloadButtonPressed?.Invoke();
             }
        }
        //  else
        // {
        //     if (reloadButtonDown == true)
        //     {
        //         reloadButtonDown = false;
        //     }
        // }
    }

    private void GetThrowInput()
    {
        if (Input.GetAxisRaw("Throw") > 0)
        {
            Debug.Log("In throw");
            if (throwButtonDown == false)
            {
                throwButtonDown = true;
                OnThrowButtonPressed?.Invoke();
            }
        }
        // else
        // {
        //     if (throwButtonDown == true)
        //     {
        //         throwButtonDown = false;
        //     }
        // }
    }

     private void GetMeleeInput()
    {
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            if (meleeButtonDown == false)
            {
                Debug.Log("In melee");
                meleeButtonDown = true;
                OnMeleeButtonPressed?.Invoke();
            }
        }
        else
        {
            if (meleeButtonDown == true)
            {
                meleeButtonDown = false;
            }
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

    private void GetDodgeInput()
    {   
        if (DodgeTimer > 0) {
            DodgeTimer -= Time.deltaTime;
        }

        // Create new Vector2 when dodge button (Left shift) pressed
        if (Input.GetAxisRaw("Dodge") > 0) {
            if (dodging == false && DodgeTimer <= 0)
            {
                DodgeTimer = .3f;
                dodging = true;
                Debug.Log("DODGE");
                OnDodgeKeyPressed?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
            }
        }
        else
        {
            if (dodging == true || DodgeTimer <= 0)
            {
                dodging = false;
            }
        }
    }

    /* IEnumerator wait()
    {
        Debug.Log("Before Wait");
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
         Debug.Log("After Wait");
    }*/
}
