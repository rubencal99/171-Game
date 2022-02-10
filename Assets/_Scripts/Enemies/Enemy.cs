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

    private AgentRenderer agentRender;

    public GameObject[] Loot; 

    private void Start()
    {
        Health = EnemyData.MaxHealth;
        Damage = EnemyData.Damage;
        Range = EnemyData.Range;
        agentRender = GetComponentInChildren<AgentRenderer>();
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
        agentRender.isDying = true;
        int odds = Random.Range(1, 20);
        if (odds == 1)
        {
            int item;
            GameObject thisLoot;
            item = Random.Range(1, 20);
            if(item < 5)
            {
                thisLoot = Instantiate(Loot[1]) as GameObject;
                thisLoot.transform.position = gameObject.transform.position;
            }
            thisLoot = Instantiate(Loot[0]) as GameObject;
            thisLoot.transform.position = gameObject.transform.position;
        }
        else
        {
            Player player = FindObjectOfType<Player>();
            player?.AddBounty(10);
        }
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
