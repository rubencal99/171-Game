using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Melee : MonoBehaviour, IWeapon
{
    [field: SerializeField]
    public Vector3 startOffset {get; set;} = Vector3.zero;
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

    [SerializeField]
    protected bool isMelee = false;

    [SerializeField]
    protected bool meleeCoroutine = false;

    /*[SerializeField]
    public string name;*/

    public Sprite sprite;
    public int comboCounter = 0;
    public float comboTime1;
    public float comboTime2;
    public float comboTimer = 0f;

    [SerializeField]
    protected float swapTime = 0.5f;
    [SerializeField]
    protected float swapTimer;

    protected void OnEnable()
    {
        swapTimer = swapTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.root.gameObject.tag == "Player"){
            isPlayer = true;
        }
        weaponParent = transform.parent.GetComponent<AgentWeapon>();
        passives = weaponParent.transform.parent.GetComponent<PlayerPassives>();
        attackPoint = transform.Find("AttackPoint");
        comboCounter = 0;
        comboTimer = 0f;
        comboTime1 = meleeData.RecoveryLength - meleeData.ComboWindow;
        comboTime2 = meleeData.RecoveryLength + meleeData.ComboWindow;
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

    public bool CheckSwap()
    {
        if(swapTimer <= 0)
        {
            return true;
        }
        return false;
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
        swapTimer -= Time.deltaTime;
        if(comboCounter > 0)
        {
            CheckComboCounter();
        }
        else if(isMelee && !meleeCoroutine)
        {
            //Debug.Log("In Melee UseWeapon");
            comboCounter++;
            OnMelee.Invoke();
            comboTimer = 0;
            Attack();
        }
        else
        {
            StopMelee();
            return;
        }
        FinishMelee();
    }

    protected void CheckComboCounter()
    {
        if(comboTimer > comboTime2)
        {
            comboCounter = 0;
            comboTimer = 0;
            return;
        }
        if(comboCounter > 0)
        {
            comboTimer += Time.deltaTime;
        }
        if(comboCounter >= meleeData.ComboLength)
        {
            return;
        }
        // If we have pressed the melee key during the combo window
        if(isMelee && comboTime1 <= comboTimer && comboTimer <= comboTime2)
        {
            comboCounter++;
            comboTimer = 0;
            Debug.Log("Combo attack triggered");
            OnMelee.Invoke();
            Attack();
        }
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
        //Debug.Log("In Melee Attack");
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
