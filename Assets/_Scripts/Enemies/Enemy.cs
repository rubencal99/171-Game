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
    public int Health { get; set; }

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
    public float deathTimer = 10.0f;

    private AgentRenderer agentRenderer;

    public AgentAnimations agentAnimations;
    private EnemyBrain enemyBrain;
    private AgentMovement agentMovement;

    private bool DontFreeze = false;
    public bool knockback;

    [SerializeField]
    private ParticleSystem blood;

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
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;
        blood.Play();
        DamageType(damageDealer);
        
        //Debug.Log("After Enemy Knockback");
        //Debug.Log("Health = " + Health);
        if (Health > 0)
        {
            OnGetHit?.Invoke();
        }
        else
        {
            StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
            //Debug.Log("Before OnDie");
            OnDie?.Invoke();
            //Debug.Log("After OnDie");
            //StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
        }
    }

    
     public void GetHit(int damage) {
         Health -= damage;
        blood.Play();
       
        //Debug.Log("After Enemy Knockback");
        Debug.Log("Health = " + Health);
        if (Health > 0)
        {
            OnGetHit?.Invoke();
        }
        else
        {
            DontFreeze = true;
            StartCoroutine(WaitToDie());
            Debug.Log("After WaitToDie coroutine");
            Debug.Log("Before OnDie");
            OnDie?.Invoke();
            Debug.Log("After OnDie");
            //StartCoroutine(WaitToDie());
            //Debug.Log("After WaitToDie coroutine");
        }
     }

   public void DamageType(GameObject damageDealer)
    {
        if(damageDealer.GetComponent<Bullet>())
        {
            BulletDataSO bulletData = damageDealer.GetComponent<Bullet>().BulletData;
            agentMovement.Knockback(bulletData.KnockbackDuration, bulletData.KnockbackPower, -damageDealer.GetComponent<Bullet>().direction);
        }
        else if(damageDealer.GetComponent<Melee>())
        {
            MeleeDataSO meleeData = damageDealer.GetComponent<Melee>().meleeData;
            var weaponPosition = damageDealer.transform.parent.position;
            var direction = transform.position - weaponPosition;
            agentMovement.Knockback(meleeData.KnockbackDelay, meleeData.KnockbackPower, -direction);
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

    private void Die()
    {

        PlayerSignaler.CallPlayerEpiBoost();
        Destroy(gameObject);
    }

    public void DeadOrAlive()
    {
        if(isDying == true)
        {
            enemyBrain.OnFireButtonReleased?.Invoke();
            gameObject.layer = 0;
            enemyBrain.Move(Vector3.zero);
            agentMovement.currentVelocity = 0;
            enemyBrain.enabled = false;
            agentRenderer.isDying = true;
            GetComponent<CapsuleCollider>().direction = 0;
        }
        else
        {
            gameObject.layer = 8;
            enemyBrain.enabled = true;
            agentRenderer.isDying = false;
            OnRevive?.Invoke();
            GetComponent<CapsuleCollider>().direction = 1;
        }
    }

}
