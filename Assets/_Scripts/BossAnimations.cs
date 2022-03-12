using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : AgentAnimations
{
    public SquidBoss boss;
    public CircleCollider2D cycloneCollider;
    public CapsuleCollider2D capsuleCollider;
    public CapsuleCollider2D movementCollider;
    void Start()
    {
        boss = transform.parent.GetComponentInChildren<SquidBoss>();
        cycloneCollider = boss.transform.gameObject.GetComponent<CircleCollider2D>();
        capsuleCollider = boss.transform.parent.gameObject.GetComponent<CapsuleCollider2D>();
        movementCollider = boss.transform.parent.transform.Find("MovementCollider").GetComponent<CapsuleCollider2D>();
    }
    public void SetCycloneAnimation(bool val)
    {
        if(val)
        {
            movementCollider.transform.localPosition = Vector3.zero;
        }
        else
        {
            movementCollider.transform.localPosition = new Vector3(0, -2, 0);
        }
        cycloneCollider.enabled = val;
        capsuleCollider.enabled = !val;
        agentAnimator.SetBool("Cyclone", val);
    }

    public void SetShootAnimation(bool val)
    {
        agentAnimator.SetBool("Shoot", val);
    }
}
