using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// IAgent has not been implemented for player yet
public class Player : MonoBehaviour, IAgent, IHittable
{
    [field: SerializeField]
    public int Health { get; private set; } = 6;

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    private Vector3 SpawnPosition;

    public GameObject obj; // game odject for agent input
    private AgentInput w; // var to hold agent input 

    private void Start()
    {
        SpawnPosition = transform.position;
        w = obj.GetComponent<AgentInput>(); //get player gameobj
    }

    public void GetHit(int damage, GameObject damageDealer)
    {    
        //check if player is Dodging, if true, dont decrement health
        if (w.dodging) {
            return;
        }

        Health--;
        // This function is supposed to play a damage animation / deliver knockback
        if (Health >= 0)    
            OnGetHit?.Invoke();
        else
        {
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());
            // Play End Game Screen here
        }
    }

    IEnumerator WaitToDie(){
        gameObject.layer = 0;
        yield return new WaitForSeconds(0.55f);
        Destroy(gameObject);
    }

    public void Respawn()
    {
        transform.position = SpawnPosition;
    }
}
