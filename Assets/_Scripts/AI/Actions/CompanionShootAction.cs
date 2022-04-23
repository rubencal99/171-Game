using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionShootAction : AIAction
{
    public Companion companion;
    public override void TakeAction()
    {
        if(companion.enemyTarget == null)
        {
            return;
        }
        // Debug.Log("In shoot");
        // This is basically the distance decision
        var distance = Vector3.Distance(companion.enemyTarget.transform.position, transform.position);
        if (Mathf.Abs(distance) < aiActionData.Range){
            enemyBrain.Attack();
        }
        else{
            enemyBrain.StopAttack();
        }
    }

    /*protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aiActionData.Range);
        Gizmos.color = Color.white;
    }*/
}
