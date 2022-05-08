using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpCube : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<ElectricCube>())
        {
            // Clear room
            Debug.Log("EMP has collided with EC");
            RatchetBoss.instance.Reset();
            RatchetBoss.inDecision = false;
            RatchetBoss.inRecovery = true;
            RatchetBoss.instance.bossAnimator.SetStunAnimation();
            RatchetBoss.instance.bossAnimator.SetRecoveryAnimation(true);
            Debug.Log("In Recovery = " + RatchetBoss.instance.InRecovery);
            Destroy(gameObject);
        }
    }
}
