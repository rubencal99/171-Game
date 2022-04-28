using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatchetBoss : _BaseBoss
{
    public AIState currentState;

    public static bool inDecision = false;
    public bool InDecision;
    public static float decisionDuration = 3f;
    public static float decisionTimer;
    public float DecisionTimer;

    public static bool inCharge = false;
    public bool InCharge;
    public static float chargeDuration = 4f;
    public static float chargeTimer;
    public float ChargeTimer;
    public static int chargeAttempts = 3;
    public int ChargeAttempts;
    public static bool hasDirection = false;
    public bool HasDirection;
    public static Vector3 chargeDirection;
    public Vector3 ChargeDirection;

    public static bool inRecovery = false;
    public bool InRecovery;
    public static float recoveryDuration = 4f;
    public static float recoveryTimer;
    public float RecoveryTimer;

    public static bool inChase = false;
    public bool InChase;
    public static float chaseDuration = 4f;
    public static float chaseTimer;
    public float ChaseTimer;

    public static bool inJump = false;
    public bool InJump;
    public static bool inAir = false;
    public bool InAir;
    public CapsuleCollider jumpCollider;


    public static bool inSpawn = false;
    public bool InSpawn;
    public static float spawnDuration = 3f;
    public static float spawnTimer;
    public float SpawnTimer;

    public BossMovement bossMovement;
    public RatchetBossAnimations bossAnimator;
    EnemyBrain brain;
    StarChaseAction StarChase;
    public AgentWeapon WeaponParent;
    public GameObject Melee;
    public GameObject LandSpray;

    public void Start()
    {
        StarChase = GetComponentInChildren<StarChaseAction>();
        Reset();
        
        brain = transform.parent.GetComponent<EnemyBrain>();
        currentState = brain.CurrentState;
        bossMovement = transform.parent.GetComponent<BossMovement>();
        bossAnimator = transform.parent.GetComponentInChildren<RatchetBossAnimations>();
        WeaponParent = transform.parent.GetComponentInChildren<AgentWeapon>();
        Melee = WeaponParent.transform.Find("Melee").gameObject;
        LandSpray = WeaponParent.transform.Find("LandSpray").gameObject;
    }

    public void Update()
    {
        currentState = brain.CurrentState;
        CheckDecision();
        if(!inDecision)
        {
            CheckCharge();
            CheckRecovery();
            CheckSpawn();
            CheckJump();
        }
    }

    public override void Reset()
    {
        //Debug.Log("In Reset");

        inDecision = true;
        InDecision = inDecision;
        decisionTimer = decisionDuration;
        DecisionTimer = decisionTimer;

        inChase = false;
        InChase = inChase;
        chaseTimer = chaseDuration;
        ChaseTimer = chaseTimer;
        //StarChase.enabled = false;

        inCharge = false;
        InCharge = inCharge;
        chargeTimer = chargeDuration;
        ChargeTimer = chargeTimer;
        hasDirection = false;
        HasDirection = hasDirection;
        chargeDirection = Vector3.zero;
        ChargeDirection = chargeDirection;

        inJump = false;
        InJump = inJump;
        inAir = false;
        InAir = inAir;

        inRecovery = false;
        InRecovery = inRecovery;
        recoveryTimer = recoveryDuration;

        inSpawn = false;
        InSpawn = inSpawn;
        spawnTimer = spawnDuration;
        SpawnTimer = spawnTimer;
    }

    public void CheckDecision()
    {
        InDecision = inDecision;
        DecisionTimer = decisionTimer;
        if(inDecision)
        {
            decisionTimer -= Time.deltaTime;
            if(decisionTimer <= 0)
            {
                inDecision = false;
                decisionTimer = decisionDuration;
            }
        }
    }

    public void CheckChase()
    {
        InChase = inChase;
        ChaseTimer = chaseTimer;
        if(inChase)
        {
            //StarChase.enabled = true;
            chaseTimer -= Time.deltaTime;
            if(LandSpray.activeSelf)
            {
                Melee.SetActive(true);
                LandSpray.SetActive(false);
                WeaponParent.AssignWeapon();
            }
            if(chaseTimer <= 0)
            {
                inChase = false;
                chaseTimer = chaseDuration;
            }
        }
        else
        {
            //StarChase.enabled = false;
        }
    }

    public void CheckCharge()
    {
        InCharge = inCharge;
        ChargeTimer = chargeTimer;
        ChargeDirection = chargeDirection;
        if(inCharge)
        {
            bossAnimator.SetChargeAnimation();
            chargeTimer -= Time.deltaTime;
            if(chargeTimer <= 0)
            {
                inCharge = false;
                chargeTimer = chargeDuration;
                chargeDirection = Vector3.zero;
            }
        }
    }
    
    public void CheckRecovery()
    {
        RecoveryTimer = recoveryTimer;
        if(inRecovery)
        {
            bossAnimator.SetStunAnimation();
            recoveryTimer -= Time.deltaTime;
            if(recoveryTimer <= 0)
            {
                recoveryTimer = recoveryDuration;
                inRecovery = false;
            }
        }
    }

    public void CheckJump()
    {
        InJump = inJump;
        if(inJump)
        {
            bossAnimator.SetSlamAnimation();
            jumpCollider.enabled = true;
            if(!LandSpray.activeSelf)
            {
                Melee.SetActive(false);
                LandSpray.SetActive(true);
                WeaponParent.AssignWeapon();
            }
        }
        else
        {
            jumpCollider.enabled = false;   
        }
    }

    public void CheckSpawn()
    {
        InSpawn = inSpawn;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SquidBoss.cycloneRange);
        Gizmos.color = Color.white;
    }
}
