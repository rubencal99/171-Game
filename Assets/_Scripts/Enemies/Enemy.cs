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

    public GameObject[] Loot;

    private AgentRenderer agentRenderer;

    private AgentAnimations agentAnimations;
    private EnemyBrain enemyBrain;
    private AgentMovement agentMovement;

    private void Start()
    {
        Health = EnemyData.MaxHealth;
        Damage = EnemyData.Damage;
        Range = EnemyData.Range;
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        enemyBrain = GetComponent<EnemyBrain>();
        agentMovement = GetComponent<AgentMovement>();
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;
        if (Health >= 0)
            OnGetHit?.Invoke();
        else
        {
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());
        }
    }

    IEnumerator WaitToDie(){
        isDying = true;
        DeadOrAlive();
        yield return new WaitForSeconds(deathTimer);
        if (isDying == true)
        {
            Die();
        }
    }

    private void Die()
    {
        int odds = Random.Range(1, 20);
        if (odds == 1)
        {
            int item;
            GameObject thisLoot;
            item = Random.Range(1, 20);
            // if(item < 5)
            // {
            //     thisLoot = Instantiate(Loot[1]) as GameObject;
            //     thisLoot.transform.position = gameObject.transform.position;
            // }
            // thisLoot = Instantiate(Loot[0]) as GameObject;
            // thisLoot.transform.position = gameObject.transform.position;
        }
        else
        {
            Player player = FindObjectOfType<Player>();
            int bounty = Random.Range(8, 15);
            player?.AddBounty(bounty);
        }
        PlayerSignaler.CallPlayerEpiBoost();
        Destroy(gameObject);
    }

    public void DeadOrAlive()
    {
        if(isDying == true)
        {
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
