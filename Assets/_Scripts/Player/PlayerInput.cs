using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public PlayerMovement PlayerMovement;
    public CapsuleCollider Collider;
    public Shop ShopKeeper;
    public LayerMask mouseColliderLayerMask;
    public Vector3 MousePos;
    public Reticule reticule;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        Collider = GetComponent<CapsuleCollider>();
        reticule.enabled = true;
        // ShopKeeper = GameObject.Find("ShopKeeper").GetComponent<Shop>();
    }

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
    public UnityEvent OnPrimaryButtonPressed { get; set; }

    // Calls PlayerWeapon.StopShooting
    [field: SerializeField]
    public UnityEvent OnPrimaryButtonReleased { get; set; }

    // Calls Player.ThowItem
    [field: SerializeField]
    public UnityEvent OnSecondaryButtonPressed { get; set; }
    [field: SerializeField]
    public UnityEvent OnSecondaryButtonReleased { get; set; }


    // Calls PlayerWeapon.StopShooting
    [field: SerializeField]
    public UnityEvent OnReloadButtonPressed { get; set; }

     [field: SerializeField]
    public UnityEvent OnThrowButtonReleased { get; set; }

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

     [field: SerializeField]
    public UnityEvent OnMapButtonPressed { get; set; }

     [field: SerializeField]
    public UnityEvent OnMapButtonReleased { get; set; }

}
