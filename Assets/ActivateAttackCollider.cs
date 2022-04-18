using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAttackCollider : StateMachineBehaviour
{
    SphereCollider attackPoint;
    Melee melee;
    public bool attack;
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //attackPoint = animator.GetComponentInChildren<SphereCollider>();
        //attackPoint.enabled = true;
        melee = animator.GetComponent<Melee>();
        attack = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        melee.Attack();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //attackPoint.enabled = false;
        attack = false;
    }

}
