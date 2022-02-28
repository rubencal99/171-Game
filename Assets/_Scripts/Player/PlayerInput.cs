using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public CapsuleCollider2D Collider;
    public Shop ShopKeeper;

    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        Collider = GetComponent<CapsuleCollider2D>();
        // ShopKeeper = GameObject.Find("ShopKeeper").GetComponent<Shop>();
    }

    // The Vector2 corresponds to the magnitude of movement in the (x, y)    wasd
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
    public UnityEvent OnMeleeButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnStandButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnInteractKeyPressed { get; set; }
    
    [field: SerializeField]
    public UnityEvent OnTabKeyPressed { get; set; }
}
