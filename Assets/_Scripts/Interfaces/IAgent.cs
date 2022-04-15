using UnityEngine;
using UnityEngine.Events;

public interface IAgent
{
    int Damage { get; }
    float Health { get; }
    UnityEvent OnDie { get; set; }
    UnityEvent OnGetHit { get; set; }

    void GetHit(float damage, GameObject damageDealer);
}