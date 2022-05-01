using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBossAnimations : AgentAnimations
{
    public SquidBoss boss;
    public SphereCollider cycloneCollider;
    public CapsuleCollider capsuleCollider;
    public CapsuleCollider movementCollider;
    void Start()
    {
        boss = transform.parent.GetComponentInChildren<SquidBoss>();
        cycloneCollider = boss.transform.gameObject.GetComponent<SphereCollider>();
        capsuleCollider = boss.transform.parent.gameObject.GetComponent<CapsuleCollider>();
        movementCollider = boss.transform.parent.transform.Find("MovementCollider").GetComponent<CapsuleCollider>();
    }
    public void SetCycloneAnimation(bool val)
    {
        /*if(val)
        {
            movementCollider.transform.localPosition = Vector3.zero;
        }
        else
        {
            movementCollider.transform.localPosition = new Vector3(0, -2, 0);
        }*/
        cycloneCollider.enabled = val;
        capsuleCollider.enabled = !val;
        agentAnimator.SetBool("Cyclone", val);
    }

    public void SetShootAnimation(bool val)
    {
        agentAnimator.SetBool("Shoot", val);
    }
}
