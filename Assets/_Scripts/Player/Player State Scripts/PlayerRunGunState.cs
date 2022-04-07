using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRunGunState : PlayerBaseState
{
    private Camera mainCamera;

    private bool primaryButtonDown = false;
    private bool throwButtonDown = false;
    private bool secondaryButtonDown = false;
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
        Debug.Log("Entered RunGun State");
        playerInput = Player.playerInput;
        mainCamera = Camera.main;
        dodging = false;
        TimeManager.RevertSlowMotion();
        Player.transform.Find("shadow").gameObject.SetActive(true);
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        GetMovementInput();
        GetPointerInput();
        GetPrimaryInput();
        GetThrowInput();
        GetSecondaryInput();
        GetReloadInput();
        // GetRestartInput();
        GetRespawnInput();
        GetDodgeInput();
        GetTabInput();
        GetInteractInput();
        if (dodging)
        {
            Debug.Log("Switching to Dive State");
            Player.SwitchState(Player.DiveState);
        }
        if (shopping)
        {
            Debug.Log("Switching to Shop State");
            Player.SwitchState(Player.ShopState);
        }
    }

    private void GetPrimaryInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (primaryButtonDown == false)
            {
                primaryButtonDown = true;
                playerInput.OnPrimaryButtonPressed?.Invoke();
            }
        }
        else
        {
            if (primaryButtonDown == true)
            {
                primaryButtonDown = false;
                playerInput.OnPrimaryButtonReleased?.Invoke();
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
        if (Input.GetAxisRaw("Throw") > 0)
        {
            if (throwButtonDown == false)
            {
                Debug.Log("throw pressed");
                throwButtonDown = true;
                playerInput.OnThrowButtonPressed?.Invoke();
            }
        }
        else
        {
            if (throwButtonDown == true)
            {
                 Debug.Log("throw released");
                throwButtonDown = false;
                 playerInput.OnThrowButtonReleased?.Invoke();
            }
        }
    }

     private void GetSecondaryInput()
    {
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            if (secondaryButtonDown == false)
            {
                // Debug.Log("In melee");
                secondaryButtonDown = true;
                playerInput.OnSecondaryButtonPressed?.Invoke();
            }
        }
        else
        {
            if (secondaryButtonDown == true)
            {
                secondaryButtonDown = false;
                playerInput.OnSecondaryButtonReleased?.Invoke();
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
        FindMousePOS();
        Vector3 mousePos = playerInput.MousePos;
        //mousePos.z = mainCamera.nearClipPlane;
        //var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // This invokes AgentRenderer.FaceDirection and PlayerWeapon.AimWeapon
        playerInput.OnPointerPositionChange?.Invoke(mousePos);
    }

    private void FindMousePOS()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, playerInput.mouseColliderLayerMask))
        {
            playerInput.MousePos = raycastHit.point;
        }
    }


    private void GetMovementInput()
    {
        playerInput.OnMovementKeyPressed?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
    }

    private void GetDodgeInput()
    {
        // Create new Vector2 when dodge button (left shift) pressed
        if (Input.GetAxisRaw("Space")  > 0)
        {
            if(PlayerAugmentations.AugmentationList["Whiskers"] && !PlayerAugmentations.AugmentationList["HookShot"]){ 
                PlayerSignaler.CallWhiskers();
            }else if(PlayerAugmentations.AugmentationList["HookShot"] && !PlayerAugmentations.AugmentationList["Whiskers"]){
                Debug.Log("HookShot is manged by script");
            }else{
                if (playerInput.PlayerMovement.currentVelocity == 0)
                {
                    return;
                }
                if (dodging == false)
                {
                    dodging = true;
                    playerInput.OnDodgeKeyPressed?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
                }
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
            Debug.Log("Tab key pressed");
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
            Debug.Log("Interact key pressed");
            if (shopping == false && playerInput.ShopKeeper.inDistance)
            {
                Debug.Log("Interact key pressed in distance of Shopkeeper");
                shopping = true;
                playerInput.OnInteractKeyPressed?.Invoke();
            }
        }
        else{
            if (shopping == true)
            {
                shopping = false;
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
