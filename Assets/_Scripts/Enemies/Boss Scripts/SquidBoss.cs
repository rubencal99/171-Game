using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SquidBoss : _BaseBoss
{
    public AIState currentState;
    public static bool inCyclone = false;
    public bool InCyclone;
    public static int cycloneAttempts = 3;
    public int CycloneAttempts;
    public static int cycloneRange = 3;
    public static bool atCycloneDest = false;
    public bool AtCycloneDest;
    public static float CycloneDuration = 4f;
    public static float cycloneTimer;
    public float CycloneTimer;


    public static bool inDecision = false;
    public bool InDecision;
    public static float decisionDuration = 3f;
    public static float decisionTimer;
    public float DecisionTimer;


    public static bool inShoot = false;
    public bool InShoot;
    public static float shootDuration = 4f;
    public static float shootTimer;
    public float ShootTimer;


    public static bool inChase = false;
    public bool InChase;
    public static float chaseDuration = 4f;
    public static float chaseTimer;
    public float ChaseTimer;


    public static bool inSpawn = false;
    public bool InSpawn;
    public static float spawnDuration = 3f;
    public static float spawnTimer;
    public float SpawnTimer;



    public BossMovement bossMovement;
    public SquidBossAnimations bossAnimator;
    EnemyBrain brain;
    StarChaseAction StarChase;
    public AgentWeapon WeaponParent;
    public GameObject Melee;
    public GameObject Spray;

    public void Start()
    {
        StarChase = GetComponentInChildren<StarChaseAction>();
        Reset();
        
        brain = transform.parent.GetComponent<EnemyBrain>();
        currentState = brain.CurrentState;
        bossMovement = transform.parent.GetComponent<BossMovement>();
        bossAnimator = transform.parent.GetComponentInChildren<SquidBossAnimations>();
        //WeaponParent = transform.parent.GetComponentInChildren<AgentWeapon>();
        if(Melee == null)
        {
            Melee = WeaponParent.transform.Find("Melee").gameObject;
        }
        if(Spray == null)
        {
            Spray = WeaponParent.transform.Find("Spray").gameObject;
        }
        
    }

    public void Update()
    {
        currentState = brain.CurrentState;
        CheckDecision();
        if(!inDecision)
        {
            CheckCyclone();
            CheckChase();
            CheckShoot();
            CheckSpawn();
        }
    }

    public override void Reset()
    {
        Debug.Log("In Reset");
        inCyclone = false;
        InCyclone = inCyclone;
        atCycloneDest = false;
        AtCycloneDest = atCycloneDest;
        cycloneAttempts = 3;
        CycloneAttempts = cycloneAttempts;
        cycloneTimer = CycloneDuration;
        CycloneTimer = cycloneTimer;

        inDecision = true;
        InDecision = inDecision;
        decisionTimer = decisionDuration;
        DecisionTimer = decisionTimer;

        inShoot = false;
        InShoot = inShoot;
        shootTimer = shootDuration;
        ShootTimer = shootTimer;

        inChase = false;
        InChase = inChase;
        chaseTimer = chaseDuration;
        ChaseTimer = chaseTimer;
        //StarChase.enabled = false;

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

    public void CheckCyclone()
    {
        InCyclone = inCyclone;
        CycloneTimer = cycloneTimer;
        CycloneAttempts = cycloneAttempts;
        AtCycloneDest = atCycloneDest;
        if(inCyclone)
        {
            bossAnimator.SetCycloneAnimation(!atCycloneDest);

            if(!atCycloneDest)
            {
                cycloneTimer -= Time.deltaTime;
                if(cycloneTimer <= 0)
                {
                    atCycloneDest = true;
                    cycloneTimer = CycloneDuration;
                    cycloneAttempts--;
                }
            }
            
        }
        else
        {
            bossAnimator.SetCycloneAnimation(false);
        }
    }

    public void CheckChase()
    {
        InChase = inChase;
        ChaseTimer = chaseTimer;
        if(inChase)
        {
            StarChase.enabled = true;
            chaseTimer -= Time.deltaTime;
            if(Spray.activeSelf)
            {
                Melee.SetActive(true);
                Spray.SetActive(false);
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
            StarChase.enabled = false;
        }
    }

    public void CheckShoot()
    {
        InShoot = inShoot;
        ShootTimer = shootTimer;
        bossAnimator.SetShootAnimation(inShoot);
        if(inShoot)
        {
            if(Melee.activeSelf)
            {
                Spray.SetActive(true);
                Melee.SetActive(false);
                WeaponParent.AssignWeapon();
            }
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0)
            {
                inShoot = false;
                shootTimer = shootDuration;
            }
        }
    }

    public void CheckSpawn()
    {
        InSpawn = inSpawn;
    }

    public void AdjustCycloneSpeed(bool val)
    {
        if(val)
        {
            inCyclone = true;
        }
        else
        {
            inCyclone = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SquidBoss.cycloneRange);
        Gizmos.color = Color.white;
    }
}
