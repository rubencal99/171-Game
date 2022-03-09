using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// IAgent has not been implemented for player yet
public class Player : MonoBehaviour, IAgent, IHittable
{
    public static Player instance;
    public RoomNode currentRoom;

    
    public bool invincible = false;

   // public int damage_iframes = 20;


    public int damage_iframes = 200;

    [field: SerializeField]
    public int Health { get; set; } = 6;

     [field: SerializeField]
    public int MaxHealth { get; private set; } = 6;

    [field: SerializeField]
    public int Wallet { get; private set; } = 80;

    [field: SerializeField]
    public int AugmentationTokens { get; private set; } = 0;

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]                         
    public bool isDead; 

    [field: SerializeField]                         
    public float getHitFrequency;

    [field: SerializeField]                         
    public float getHitIntensity; 
    
    [field: SerializeField]                         
    public float getHitTime;                           //For debug

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

     [field: SerializeField]
    public UnityEvent OnHeal { get; set; }

    [field: SerializeField]
    public GameObject DeathMenuUI;

    private Vector3 SpawnPosition;

    [SerializeField]
    private ParticleSystem blood;

    [SerializeField]
    private Image overlay;

    private AgentRenderer agentRenderer;
    [SerializeField]


    public PlayerStateManager PlayerState; // game odject for agent input
    // private AgentInput w; // var to hold agent input 
// =======
//     private AgentRenderer agentRender;
// >>>>>>> master

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Note, resetting augmentations will want to be moved somewhere else
        // if incorporating multiple levels
        PlayerAugmentations.ResetAugmentations();
        SpawnPosition = transform.position;
        PlayerState = GetComponent<PlayerStateManager>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        //DeathMenuUI.SetActive(false);
        isDead = false;                                         //Debuging death 
        blood = GameObject.Find("PlayerBlood").GetComponent<ParticleSystem>();
        overlay = GameObject.Find("Overlay").GetComponent<Image>();
    }

    private void Update()
    {
         if (isDead==true){                      //For Debug the instance kill 
             Health -= Health;
             OnDie?.Invoke();
             StartCoroutine(WaitToDie());
         }
         setOverlay();
    }

    private void setOverlay(){
        var curHealth = (float)Health/MaxHealth;
        //Debug.Log("health/MaxHealth: " + curHealth);
        var tempAlpha = (float)(1f - curHealth);
        var tempOverlay = overlay.color;
        tempOverlay.a = tempAlpha;
        overlay.color = tempOverlay;

        StartCoroutine(fadeOverlay());
        //Debug.Log("Now 1 - health/Maxhealth: " + tempAlpha);
        //Debug.Log("What alpha should be: " + (float)(1.0 - (float)(Health/MaxHealth)));
    }

    private IEnumerator fadeOverlay() {
        var tempOverlay = overlay.color;
        for (float alpha = tempOverlay.a; alpha >= 0; alpha -= 0.1f)
        {
           Debug.Log("fading overlay, alpha = " + alpha);
           if(alpha < 0.1f) alpha = 0;
            tempOverlay.a = alpha;
             overlay.color = tempOverlay;
            yield return null;
        }
    }

    public void Heal(int amount) {
        Health += amount;
        if(Health > MaxHealth)
            Health = MaxHealth;
    }

    public void setMaxHp(int amount) {
        MaxHealth = amount;

    }


    public void GetHit(int damage, GameObject damageDealer)
    {    
        if(invincible)
        {
            return;
        }
        //check if player is Dodging, if true, dont decrement health
        if (PlayerState.DiveState.diving) {
            return;
        }

        Health -= damage;
        blood.Play();
        CameraShake.Instance.ShakeCamera((float)damage * getHitIntensity, getHitFrequency, getHitTime);
        if (Health > 0) {   
            OnGetHit?.Invoke();
            StartCoroutine(iframes_damage());
        }
        else
        {
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());   
        }
    }


    public void Heal()
    {
        Health += 2;
    }

    public void AddBounty(int funds)
    {
        Wallet += funds;
    }

    public bool CanPurchase(int cost)
    {
        if (cost > Wallet)
        {
            return false;
        }
        return true;
    }

    public void Purchase(int cost)
    {
        Wallet -= cost;
    }

    public bool CanAcuire(int cost)
    {
        if (AugmentationTokens < cost)
        {
            return false;
        }
        return true;
    }

    public void Acquire(int cost)
    {
        AugmentationTokens -= cost;
    }
    IEnumerator WaitToDie(){
        gameObject.layer = 0;
        agentRenderer.isDying = true;
        yield return new WaitForSeconds(3f);
        //Destroy(gameObject);
        // Play End Game Screen here
        DeathMenuUI.SetActive(true);
    }


    public void Respawn()
    {
        transform.position = SpawnPosition;
    }

     public IEnumerator iframes_damage() {
        invincible = true;
        yield return new WaitForSeconds((float)damage_iframes / 60f);
        invincible = false;
    }
}
