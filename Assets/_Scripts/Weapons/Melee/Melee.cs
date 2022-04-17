using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Melee : MonoBehaviour, IWeapon
{
    public Transform attackPoint;
    public float attackRange = 0.15f;
    protected AgentWeapon weaponParent;

    [SerializeField]
    public PlayerPassives passives;
    public LayerMask enemyLayers;

    // WeaponDataSO Holds all our weapon data
    [SerializeField]
    public MeleeDataSO meleeData;

    [SerializeField]
    public bool isPlayer;

    protected bool isMelee = false;

    [SerializeField]
    protected bool meleeCoroutine = false;

    /*[SerializeField]
    public string name;*/

    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.root.gameObject.tag == "Player"){
            isPlayer = true;
        }
        weaponParent = transform.parent.GetComponent<AgentWeapon>();
        passives = weaponParent.transform.parent.GetComponent<PlayerPassives>();
        attackPoint = transform.Find("AttackPoint");
    }

    [field: SerializeField]
    public UnityEvent OnMelee { get; set; }

    public virtual float getMeleeSpeed() {
        // Here we'll use the PlayerSignaller to find melee speed
        throw new NotImplementedException();
    }

    public void TryMelee()
    {
        isMelee = true;
    }
    public void StopMelee()
    {
        isMelee = false;
    }

    public void ForceReload() {
        meleeCoroutine = false;
        GetComponent<SpriteRenderer>().sprite = sprite;
        //GetComponent<Animator>().Play("idle");
    }

    protected void Update()
    {
        UseWeapon();
    }

    protected virtual void UseWeapon()
    {
        if(isMelee && !meleeCoroutine)
        {
            OnMelee.Invoke();
            Attack();
        }
        else
        {
            StopMelee();
            return;
        }
        FinishMelee();
    }

    private void FinishMelee()
    {
        StartCoroutine(DelayNextMeleeCoroutine());
        
        isMelee = false;
    }

    protected IEnumerator DelayNextMeleeCoroutine()
    {
        meleeCoroutine = true;
        yield return new WaitForSeconds(meleeData.RecoveryLength / passives.ROFMultiplier);
        meleeCoroutine = false;
    }

    public void Attack()
    {
        // Play attack animation

        // Detect enemies in range of attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);


        // Damage enemies
        foreach(Collider enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            HitEnemy(enemy);
        }

    }

    public void HitEnemy(Collider collision)
    {
        // This drops enemy health / destroys enemy
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(meleeData.Damage, gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
