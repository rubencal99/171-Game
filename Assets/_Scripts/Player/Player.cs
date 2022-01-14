using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// IAgent has not been implemented for player yet
public class Player : MonoBehaviour, IAgent
{
    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health--;
        // This function is supposed to play a damage animation / deliver knockback
        if (Health >= 0)    
            OnGetHit?.Invoke();
        else
        {
            OnDie?.Invoke();
            Destroy(gameObject);
            // Play End Game Screen here
        }
    }
}
