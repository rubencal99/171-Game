using UnityEngine;
using UnityEngine.Events;

public interface IAgent
{
    int Damage { get; }
    int Health { get; }
    UnityEvent OnDie { get; set; }
    UnityEvent OnGetHit { get; set; }

    void GetHit(int damage, GameObject damageDealer);
}