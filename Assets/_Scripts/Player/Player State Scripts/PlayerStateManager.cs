using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerInput playerInput;
    [SerializeField]
    public PlayerBaseState currentState;
    public PlayerRunGunState RunGunState = new PlayerRunGunState();
    public PlayerDiveState DiveState = new PlayerDiveState();
    public PlayerProneState ProneState = new PlayerProneState();
    public PlayerShopState ShopState = new PlayerShopState();

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
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
