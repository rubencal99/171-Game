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
    public int Health { get; private set; }

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public float Range { get; private set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    private AgentRenderer agentRenderer;
    private EnemyBrain enemyBrain;

    private void Start()
    {
        Health = EnemyData.MaxHealth;
        Damage = EnemyData.Damage;
        Range = EnemyData.Range;
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        enemyBrain = GetComponent<EnemyBrain>();
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
        gameObject.layer = 0;
        agentRenderer.isDying = true;
        DeadOrAlive();
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void DeadOrAlive()
    {
        if(agentRenderer.isDying == true)
        {
            enemyBrain.enabled = false;
        }
        else
        {
            enemyBrain.enabled = true;
        }
    }

}
