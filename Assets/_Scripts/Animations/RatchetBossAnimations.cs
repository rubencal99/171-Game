using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatchetBossAnimations : AgentAnimations
{
    public RatchetBoss boss;
    public CapsuleCollider chargeCollider;
    public BoxCollider boxCollider;
    public CapsuleCollider movementCollider;
    public EnemyGun GroundSlam;
    public EnemyGun PoundGun;
    public EnemyGun ChargeSpray;
    void Start()
    {
        boss = transform.parent.GetComponentInChildren<RatchetBoss>();
        //chargeCollider = boss.transform.gameObject.GetComponent<CapsuleCollider>();
        boxCollider = boss.transform.parent.gameObject.GetComponent<BoxCollider>();
        movementCollider = boss.transform.parent.transform.Find("MovementCollider").GetComponent<CapsuleCollider>();
    }

    public void Slam()
    {
        //GroundSlam.enabled = true;
        GroundSlam.ForceReload();
        GroundSlam.TryShooting();
    }

    public void Pound()
    {
        Debug.Log("In Pound");
        if(PoundGun.gameObject.activeSelf == false)
        {
            GroundSlam.gameObject.SetActive(false);
            PoundGun.gameObject.SetActive(true);
            ChargeSpray.gameObject.SetActive(false);
            //WeaponParent.AssignWeapon();
        }
        PoundGun.ForceReload();
        PoundGun.TryShooting();
    }

    public void TransitionChargeWeapon()
    {
        Debug.Log("In Transition Charge Weapon");
        RatchetBoss.inChargeStart = false;
    }

    public void SetChargeAnimation()
    {
        agentAnimator.SetTrigger("Charge");
    }

    public void SetStunAnimation()
    {
        Debug.Log("Boss Stunned");
        agentAnimator.SetTrigger("Stunned");
    }

    public void SetIdleAnimation()
    {
        agentAnimator.SetTrigger("Idle");
    }

    public void SetSlamAnimation()
    {
        agentAnimator.SetTrigger("GroundSlam");
    }

    //public void SetCycloneAnimation(bool val)
    //{
        /*if(val)
        {
            movementCollider.transform.localPosition = Vector3.zero;
        }
        else
        {
            movementCollider.transform.localPosition = new Vector3(0, -2, 0);
        }*/
        /*cycloneCollider.enabled = val;
        capsuleCollider.enabled = !val;
        agentAnimator.SetBool("Cyclone", val);
    }*/

    public void SetShootAnimation(bool val)
    {
        agentAnimator.SetBool("Shoot", val);
    }
}
