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
    public bool isDead;                             //For debug

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public GameObject DeathMenuUI;

    [field: SerializeField]
    public GameObject PauseMenuUI;

    private Vector3 SpawnPosition;

    private AgentRenderer agentRender;

    private void Start()
    {
        SpawnPosition = transform.position;
        DeathMenuUI.SetActive(false);
        PauseMenuUI.SetActive(false);
        agentRender = GetComponentInChildren<AgentRenderer>();
        isDead = false;
    }
    private void Update()
    {
        if (isDead==true){                      //For Debug
            Health -= Health;                   //make a instance kill 
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());
        }
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;
        // This function is supposed to play a damage animation / deliver knockback
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
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        // Play End Game Screen here
        DeathMenuUI.SetActive(true);
    }

    public void Respawn()
    {
        transform.position = SpawnPosition;
    }
}
