using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRunGunState : PlayerBaseState
{
    private Camera mainCamera;

    private bool fireButtonDown = false;
    private bool throwButtonDown = false;
    private bool meleeButtonDown = false;
    private bool tabButtonDown = false;

    public bool dodging = false; // bool to check if dodging
    public bool shopping = false; // bool to check if dodging

    public PlayerInput playerInput;
    
    [SerializeField]
    public float DodgeTimer;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered RunGun State");
        TimeManager.RevertSlowMotion();
        playerInput = Player.playerInput;
        mainCamera = Camera.main;
        dodging = false;
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetThrowInput();
        GetMeleeInput();
        GetReloadInput();
        // GetRestartInput();
        // GetRespawnInput();
        GetDodgeInput();
        GetTabInput();
        if (dodging)
        {
            Debug.Log("Switching to Dive State");
            Player.SwitchState(Player.DiveState);
        }
        if (shopping)
        {
            Debug.Log("Switching to Shop State");
            Player.SwitchState(Player.DiveState);
        }
    }



    /*********************************
    NOTE:
    This script functions exactly like AgentInput
    It will replace AgentInput for the Player object so that we can use UpdateState
    instead of Update()
    **********************************/



    /*// The Vector2 corresponds to the magnitude of movement in the (x, y)    wasd
    // (0, 0), (0, 1), (1, 0), (1, 1), (0, -1), (-1, 0), (-1, -1), (1, -1), (-1, 1)
    // Passes the Vector2 to AgentMovement.MoveAgent hence SerializeField
    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    // Vector2 coresponds to the position of the mouse on the screen
    // This funciton is used to aim the weapon and change player direction
    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    // Dodge Mechanic
    // Vector2 Corresponds towards the movement of where the dodge roll happens
    // *************************
    [field: SerializeField]
    public UnityEvent<Vector2> OnDodgeKeyPressed { get; set; }
    // *************************

    // Calls PlayerWeapon.shoot
    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    // Calls PlayerWeapon.StopShooting
    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    // Calls PlayerWeapon.StopShooting
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
    public UnityEvent OnMeleeButtonPressed { get; set; }*/

    /* private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetThrowInput();
        GetMeleeInput();
        GetReloadInput();
        // GetRestartInput();
        GetRespawnInput();
        GetDodgeInput();
    }*/

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

    private void GetThrowInput()
    {
        if (Input.GetAxisRaw("Fire3") > 0)
        {
            if (throwButtonDown == false)
            {
                Debug.Log("In throw");
                throwButtonDown = true;
                playerInput.OnThrowButtonPressed?.Invoke();
            }
        }
        else
        {
            if (throwButtonDown == true)
            {
                throwButtonDown = false;
            }
        }
    }

     private void GetMeleeInput()
    {
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            if (meleeButtonDown == false)
            {
                // Debug.Log("In melee");
                meleeButtonDown = true;
                playerInput.OnMeleeButtonPressed?.Invoke();
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
            playerInput.OnRestartButtonPressed?.Invoke();
        }
    }

    private void GetRespawnInput()
    {
        if (Input.GetAxisRaw("Space") > 0)
        {
            Debug.Log("In Respawn Input");
            // This will respawn the player to their original position at start of scene
            playerInput.OnRespawnButtonPressed?.Invoke();
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

    private void GetDodgeInput()
    {
        // Create new Vector2 when dodge button (left shift) pressed
        if (Input.GetAxisRaw("Dodge") > 0) 
        {
            if (playerInput.PlayerMovement.currentVelocity == 0)
            {
                return;
            }
            if (dodging == false)
            {
                dodging = true;
                playerInput.OnDodgeKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            }  
        }
        else{
            if (dodging == true)
            {
                dodging = false;
            }
        }
    }

    private void GetTabInput()
    {
        if (Input.GetAxisRaw("Tab") > 0)
        {
            if (tabButtonDown == false)
            {
                tabButtonDown = true;
                playerInput.OnTabKeyPressed?.Invoke();
            }
            // Debug.Log("Tab key pressed");
        }
        else
        {
            if (tabButtonDown == true)
            {
                tabButtonDown = false;
            }
        }
    }

    private void GetInteractInput()
    {
        // Create new Vector2 when dodge button (left shift) pressed
        if (Input.GetAxisRaw("Interact") > 0) 
        {
            if (shopping == false)
            {
                shopping = true;
                playerInput.OnInteractKeyPressed?.Invoke();
            }  
        }
        else{
            if (dodging == true)
            {
                dodging = false;
            }
        }
    }

    /*private void GetDodgeInput()
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
                OnDodgeKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            }
        }
        else
        {
            if (dodging == true || DodgeTimer <= 0)
            {
                dodging = false;
            }
        }
    }*/
}
