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
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    private void Start()
    {
        Health = EnemyData.MaxHealth;
        Damage = EnemyData.Damage;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health--;
        if (Health >= 0)
            OnGetHit?.Invoke();
        else
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }
    }

}
