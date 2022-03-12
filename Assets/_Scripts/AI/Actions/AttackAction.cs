using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    GameObject self;
     void Start(){
        self = this.transform.root.gameObject;
    }
    public override void TakeAction()
    {
        // aiMovementData.Direction = Vector2.zero;
        aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        // enemyBrain.Move(aiMovementData.Direction);
        enemyBrain.Aim(aiMovementData.PointOfInterest);
        aiActionData.Attack = true;
         self.GetComponentInChildren<AgentAnimations>().SetAttackAnimation();
        enemyBrain.Attack();
        //enemyBrain.StopAttack();
    }
}
