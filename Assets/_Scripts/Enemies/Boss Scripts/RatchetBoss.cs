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

    public static bool inChargeStart = false;
    public bool InChargeStart;
    public static bool inCharge = false;
    public bool InCharge;
    public static float chargeDuration = 8f;
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

    public static bool inSlam = false;
    public bool InSlam;
    public static float slamDuration = 3f;
    public static float slamTimer;
    public float SlamTimer;

    public BossMovement bossMovement;
    public RatchetBossAnimations bossAnimator;
    EnemyBrain brain;
    StarChaseAction StarChase;
    public AgentWeapon WeaponParent;
    public GameObject GroundSlam;
    public GameObject PoundGun;
    public GameObject ChargeSpray;

    public void Start()
    {
        StarChase = GetComponentInChildren<StarChaseAction>();
        Reset();
        
        brain = transform.parent.GetComponent<EnemyBrain>();
        currentState = brain.CurrentState;
        bossMovement = transform.parent.GetComponent<BossMovement>();
        bossAnimator = transform.parent.GetComponentInChildren<RatchetBossAnimations>();
        WeaponParent = transform.parent.GetComponentInChildren<AgentWeapon>();
        if(GroundSlam == null)
        {
            GroundSlam = WeaponParent.transform.Find("GroundSlam").gameObject;
        }
        if(PoundGun == null)
        {
            PoundGun = WeaponParent.transform.Find("Pound").gameObject;
        }
        if(ChargeSpray == null)
        {
            ChargeSpray = WeaponParent.transform.Find("ChaseSpray").gameObject;
        }
    }

    public void Update()
    {
        currentState = brain.CurrentState;
        CheckDecision();
        if(!inDecision)
        {
            CheckSlam();
            CheckCharge();
            CheckRecovery();
            CheckSpawn();
            CheckJump();
        }
    }

    public override void Reset()
    {
        //Debug.Log("In Reset");
        bossAnimator.SetIdleAnimation();

        inDecision = true;
        InDecision = inDecision;
        decisionTimer = decisionDuration;
        DecisionTimer = decisionTimer;

        inSlam = false;
        InSlam = inSlam;
        slamTimer = slamDuration;
        SlamTimer = slamTimer;

        inChase = false;
        InChase = inChase;
        chaseTimer = chaseDuration;
        ChaseTimer = chaseTimer;
        //StarChase.enabled = false;

        inChargeStart = false;
        InChargeStart = inChargeStart;
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

    public void CheckSlam()
    {
        InSlam = inSlam;
        SlamTimer = slamTimer;
        if(inSlam)
        {
            if(slamTimer == slamDuration)
            {
                bossAnimator.SetSlamAnimation();
            }
            if(GroundSlam.activeSelf == false)
            {
                GroundSlam.SetActive(true);
                PoundGun.SetActive(false);
                ChargeSpray.SetActive(false);
                WeaponParent.AssignWeapon();
            }
            slamTimer -= Time.deltaTime;
            if(slamTimer <= 0)
            {
                inSlam = false;
                slamTimer = slamDuration;
            }
        }
    }

    /*public void CheckChase()
    {
        InChase = inChase;
        ChaseTimer = chaseTimer;
        if(inChase)
        {
            //StarChase.enabled = true;
            chaseTimer -= Time.deltaTime;
            if(LandSpray.activeSelf)
            {
                GroundSlam.SetActive(true);
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
    }*/

    public void CheckCharge()
    {
        InChargeStart = inChargeStart;
        InCharge = inCharge;
        ChargeTimer = chargeTimer;
        ChargeDirection = chargeDirection;
        if(InChargeStart)
        {
            if(PoundGun.activeSelf == false)
            {
                GroundSlam.SetActive(false);
                PoundGun.SetActive(true);
                ChargeSpray.SetActive(false);
                //WeaponParent.AssignWeapon();
            }
            bossAnimator.SetChargeAnimation();
        }
        else if(inCharge)
        {
            if(ChargeSpray.activeSelf == false)
            {
                GroundSlam.SetActive(false);
                PoundGun.SetActive(false);
                ChargeSpray.SetActive(true);
                WeaponParent.AssignWeapon();
            }
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
            //bossAnimator.SetSlamAnimation();
            bossMovement.rigidbody.useGravity = false;
            jumpCollider.enabled = true;
            if(!PoundGun.activeSelf)
            {
                GroundSlam.SetActive(false);
                PoundGun.SetActive(true);
                WeaponParent.AssignWeapon();
            }
        }
        else
        {
            jumpCollider.enabled = false;
            bossMovement.rigidbody.useGravity = true;
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
