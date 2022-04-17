using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidesWithWallDecision : AIDecision
{
    public bool hasCollided;
    public override bool MakeADecision()
    {
        if(hasCollided)
        {
            RatchetBoss.chargeDirection = Vector3.zero;
            RatchetBoss.hasDirection = false;
            RatchetBoss.inRecovery = true;
            hasCollided = false;
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Debug.Log("In Ratchet Collider Trigger");
            hasCollided = true;
        }
    }
}
