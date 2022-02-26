using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// IAgent has not been implemented for player yet
public class Player : MonoBehaviour, IAgent, IHittable
{
    [field: SerializeField]
    public int Health { get; set; } = 6;

     [field: SerializeField]
    public int MaxHealth { get; private set; } = 6;

    [field: SerializeField]
    public int Wallet { get; private set; } = 0;

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
    private AgentRenderer agentRenderer;

    public ParticleSystem blood;

    public Image image;
    private float tempAlpha;

    public PlayerStateManager PlayerState; // game odject for agent input
    // private AgentInput w; // var to hold agent input 
// =======
//     private AgentRenderer agentRender;
// >>>>>>> master

    private void Start()
    {
        SpawnPosition = transform.position;
        PlayerState = GetComponent<PlayerStateManager>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        DeathMenuUI.SetActive(false);
        isDead = false;
        image = GetComponent<Image>();
        tempAlpha = 0f;
        //Debuging death 
    }

    private void Update()
    {
         if (isDead==true){                      //For Debug the instance kill 
             Health -= Health;
             OnDie?.Invoke();
             StartCoroutine(WaitToDie());
         }
        //Blood screen overlay
        var tempOverlay = image.color;
        tempOverlay.a = 1 - Health/MaxHealth;
        image.color = tempOverlay;
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
        //check if player is Dodging, if true, dont decrement health
        if (PlayerState.DiveState.diving) {
            return;
        }

        blood.Play();
        Health -= damage;
        CameraShake.Instance.ShakeCamera(damage * getHitIntensity, getHitFrequency, getHitTime);
        blood.Stop();

// =======
//         
//         DeathMenuUI.SetActive(false);
//         agentRender = GetComponentInChildren<AgentRenderer>();
//         isDead = false;   
//     }
//     private void Update()
//     {
//         if (isDead==true){                      //For Debug
//             Health = 0;
//             OnDie?.Invoke();
//             StartCoroutine(WaitToDie());
//         }
//     }

//     public void GetHit(int damage, GameObject damageDealer)
//     {
//         Health -= damage;
// >>>>>>> master
        // This function is supposed to play a damage animation / deliver knockback
        if (Health >= 0)    
            OnGetHit?.Invoke();
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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        // Play End Game Screen here
        DeathMenuUI.SetActive(true);
    }


    public void Respawn()
    {
        transform.position = SpawnPosition;
    }
}
