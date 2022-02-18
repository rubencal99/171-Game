using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This state is responsible for when the player is Diving
// This state should only be active for a fraction of a second (unless we develop upgrades like slowmo bullet time while diving)
// This state automatically transitions to prone state
public class PlayerDiveState : PlayerBaseState
{
    public bool diving;
    [SerializeField]
    private float DiveTimer = 0.3f;
    private float diveTime;
    public PlayerInput playerInput;
    public CapsuleCollider2D collider;
    public override void EnterState(PlayerStateManager Player)
    {
        // Debug.Log("Entered Dive State");
        diving = true;
        diveTime = DiveTimer;
        playerInput = Player.playerInput;
        collider = playerInput.Collider;
        collider.enabled = false;
    }

    public override void UpdateState(PlayerStateManager Player)
    {
        CalculateDiveTime();
        GetMovementInput();
        if (diveTime <= 0)
        {
            diving = false;
            collider.enabled = true;
            Player.SwitchState(Player.ProneState);
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
