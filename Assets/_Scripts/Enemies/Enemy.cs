using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Mostly responsible for enemy collisions & data storage
public class Enemy : MonoBehaviour, IHittable, IAgent
{
    [field: SerializeField]
    public EnemyDataSO EnemyData { get; set; }

    [field: SerializeField]
    public float Health { get; set; }
    [field: SerializeField]
    int tempHealth {get; set;}

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public float Range { get; private set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnRevive { get; set; }
    public bool isDying = false;
    public bool hasDied = false;
    public float deathTimer = 10.0f;

    private AgentRenderer agentRenderer;

    public AgentAnimations agentAnimations;
    private EnemyBrain enemyBrain;
    public AgentMovement agentMovement;

    private bool DontFreeze = false;
    public bool knockback;
    public bool hasProtector = false;

    [SerializeField]
    private ParticleSystem blood;

    public float timer;

    private void Start()
    {
        Health = EnemyData.MaxHealth;
        Damage = EnemyData.Damage;
        Range = EnemyData.Range;
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        enemyBrain = GetComponent<EnemyBrain>();
        agentMovement = GetComponent<AgentMovement>();
        blood = transform.Find("EnemyBlood").GetComponent<ParticleSystem>();
        //blood = FindComponentInChildWithTag
        timer = 0;
    }

    public virtual void Update(){
        DeadOrAlive();
    }

    public virtual void GetHit(float damage, GameObject damageDealer)
    {
        float d = PlayerSignaler.CallDamageBuff(damage);
        tempHealth = (int)Health;
        Health -= d;
        
        //Debug.Log("After Enemy Knockback");
        //Debug.Log("Health = " + Health);
        if (Health > 0 && (int)Health != tempHealth)
        {
             if(blood != null)
                blood.Play();
            DamageType(damageDealer);
            OnGetHit?.Invoke();
        }
        else if (Health <= 0)
        {
            if(blood != null)
                blood.Play();
            DamageType(damageDealer);
            StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
            //Debug.Log("Before OnDie");
            OnDie?.Invoke();
            //Debug.Log("After OnDie");
            //StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
        }
    }

    
     public void GetHit(float damage) {
         Health -= damage;
        blood.Play();
        
        //Debug.Log("After Enemy Knockback");
        //Debug.Log("Health = " + Health);
        if (Health > 0)
        {
            OnGetHit?.Invoke();
        }
        else
        {
            DontFreeze = true;
            StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
            //Debug.Log("Before OnDie");
            OnDie?.Invoke();
            //Debug.Log("After OnDie");
            //StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
        }
     }

   public void DamageType(GameObject damageDealer)
    {
        float KOStrength = PlayerSignaler.CallElephantStrength();
        if(damageDealer.GetComponent<Bullet>())
        {
            BulletDataSO bulletData = damageDealer.GetComponent<Bullet>().BulletData;
            agentMovement.Knockback(bulletData.KnockbackDuration, bulletData.KnockbackPower * KOStrength, -damageDealer.GetComponent<Bullet>().direction);
        }
        else if(damageDealer.GetComponent<Melee>())
        {
            MeleeDataSO meleeData = damageDealer.GetComponent<Melee>().meleeData;
            var weaponPosition = damageDealer.transform.parent.position;
            var direction = transform.position - weaponPosition;
            agentMovement.Knockback(meleeData.KnockbackDelay, meleeData.KnockbackPower * KOStrength, -direction);
        }else if(damageDealer.GetComponent<Player>()){//Collider
            Debug.Log("Should push back enemy on touching thorns collider");
            var direction = transform.position - damageDealer.transform.position;
            agentMovement.Knockback(PlayerAugmentations.ThornKO, PlayerAugmentations.ThornPushAmount * KOStrength, -direction); //This needs tweaking
        }
    }


    IEnumerator WaitToDie(){
        Debug.Log("In wait to die");
        isDying = true;
        DeadOrAlive();
        int odds = Random.Range(1, 100);
        if(!DontFreeze)
            TimeManager.Instance.Freeze();
        DontFreeze = false;
        if (odds > 60) 
        {
            Loot thisLoot = FindObjectOfType<Loot>();
            thisLoot?.Pick(gameObject);
        }
        else
        {
            Player player = FindObjectOfType<Player>();
            int bounty = Random.Range(8, 15);
            player?.AddBounty(bounty);
        }
        yield return new WaitForSeconds(deathTimer);
        if (isDying == true)
        {
            Die();
        }
    }

    public void Die()
    {
        if(PlayerAugmentations.AugmentationList["Predator"]){
            PlayerSignaler.usePredator = true;
        }
        PlayerSignaler.CallPlayerEpiBoost();
        Destroy(gameObject);
    }

    public void DeadOrAlive()
    {
        if(isDying)
        {
            if(enemyBrain != null)
                enemyBrain.enabled = false;
            if(!hasDied)
            {
                enemyBrain.OnFireButtonReleased?.Invoke();
                gameObject.layer = 0;
                if(enemyBrain != null)
                    enemyBrain.Move(Vector3.zero);
                agentMovement.currentVelocity = 0;
                agentRenderer.isDying = true;
                //GetComponent<CapsuleCollider>().direction = 0;
                hasDied = true;
            }
        }
        else
        {
            if(hasDied)
            {
                gameObject.layer = 8;
                agentRenderer.isDying = false;
                OnRevive?.Invoke();
                //GetComponent<CapsuleCollider>().direction = 1;
                if(enemyBrain != null)
                    enemyBrain.enabled = true;
                hasDied = false;
            }
            
        }
    }

    public void OnCollisionEnter(Collision collision){
        // if(collision.gameObject.CompareTag("Thorns")){
        //     GetHit(PlayerAugmentations.ThornDam, collision.gameObject);
        // }
    }

    // This is only used by BodyGuards
    public void ResetProtector()
    {
        enemyBrain.Target.GetComponent<Enemy>().hasProtector = false;
    }

}
