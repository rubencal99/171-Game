using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager instance;
    public PlayerInput playerInput;
    [SerializeField]
    public PlayerBaseState currentState;
    public PlayerRunGunState RunGunState = new PlayerRunGunState();
    public PlayerDiveState DiveState = new PlayerDiveState();
    public PlayerProneState ProneState = new PlayerProneState();
    public PlayerShopState ShopState = new PlayerShopState();
    public PlayerTabState TabState = new PlayerTabState();

    public float interactKeyTimer = 0.5f;
    public float interactKeyTime = 0.5f;
    public bool InteractKeyPressed = false;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        currentState = RunGunState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInteractTimer();
        if(!Player.instance.isDead)
        {
            currentState.UpdateState(this);
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void UpdateInteractTimer()
    {
        if(InteractKeyPressed)
        {
            interactKeyTimer -= Time.deltaTime;
            if(interactKeyTimer <= 0)
            {
                interactKeyTimer = interactKeyTime;
                InteractKeyPressed = false;
            }
        }
    }
}
