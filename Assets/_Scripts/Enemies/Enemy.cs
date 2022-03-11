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
    public bool isDying = false;
    public float deathTimer = 10.0f;

    private AgentRenderer agentRenderer;

    public AgentAnimations agentAnimations;
    private EnemyBrain enemyBrain;
    private AgentMovement agentMovement;
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
        BulletDataSO bulletData = damageDealer.GetComponent<Bullet>().BulletData;
        Debug.Log("In Enemy Get Hit");
        Debug.Log("Bullet KDuration: " + bulletData.KnockbackDuration);
        Debug.Log("Bullet KPower: " + bulletData.KnockbackPower);

        // Temp();
        agentMovement.Knockback(bulletData.KnockbackDuration, bulletData.KnockbackPower, -damageDealer.GetComponent<Bullet>().direction);
        
        Debug.Log("After Enemy Knockback");
        if (Health >= 0)
            OnGetHit?.Invoke();
        else
        {
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());
        }
    }

    public void Temp()
    {
        Debug.Log("Temp test");
    }


    IEnumerator WaitToDie(){
        isDying = true;
        DeadOrAlive();
        int odds = Random.Range(1, 100);
        if (odds > 25) 
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
            enemyBrain.Move(Vector2.zero);
            agentMovement.currentVelocity = 0;
            enemyBrain.enabled = false;
            agentRenderer.isDying = true;
        }
        else
        {
            gameObject.layer = 8;
            enemyBrain.enabled = true;
            agentRenderer.isDying = false;
        }
    }

}
