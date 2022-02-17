using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This state is responsible for when the player is Diving
// This state should only be active for a fraction of a second (unless we develop upgrades like slowmo bullet time while diving)
// This state automatically transitions to prone state
public class PlayerDiveState : PlayerBaseState
{
    private Camera mainCamera;

    private bool fireButtonDown = false;

    public bool diving;
    [SerializeField]
    private float DiveTimer = 0.3f;
    private float diveTime;
    public PlayerInput playerInput;
    public CapsuleCollider2D collider;
    float m_ScaleX, m_ScaleY, m_ScaleZ;
    //public Slider m_SliderX, m_SliderY, m_SliderZ;
    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered Dive State");
        mainCamera = Camera.main;
        diving = true;
        diveTime = DiveTimer;
        playerInput = Player.playerInput;
        collider = playerInput.Collider;
        collider.enabled = false;
        PlayerSignaler.CallBulletTime();
        Debug.Log("Current collider size:" + collider.size);
        //collider.enabled = false;
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        CalculateDiveTime();
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetReloadInput();
        if (diveTime <= 0)
        {
            diving = false;
            collider.enabled = true;
            Player.SwitchState(Player.ProneState);
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

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // This invokes AgentRenderer.FaceDirection and PlayerWeapon.AimWeapon
        playerInput.OnPointerPositionChange?.Invoke(mouseInWorldSpace);
    }

    private void GetReloadInput()
    {
        if (Input.GetAxisRaw("Reload") > 0)
        {
            playerInput.OnReloadButtonPressed?.Invoke();
        }
    }

    private void CalculateDiveTime()
    {
        if (diveTime > 0)
        {
            diveTime -= Time.deltaTime;
        }
    }

    private void GetMovementInput()
    {
        playerInput.OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
