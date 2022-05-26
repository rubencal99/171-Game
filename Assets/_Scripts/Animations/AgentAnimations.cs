using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimations : MonoBehaviour
{
    protected Animator agentAnimator;

    protected void Awake()
    {
        agentAnimator = GetComponent<Animator>();
    }

    public void SetIdleAnimation()
    {
        agentAnimator.SetTrigger("Idle");
    }

    public void SetWalkAnimation(bool val)
    {
        agentAnimator.SetBool("Walk", val);
    }

    public void SetAttackAnimation()
    {
        agentAnimator.SetTrigger("Attack");
    }

    public void SetDodgeAnimation()
    {
        agentAnimator.SetTrigger("dodge");
         //Debug.Log("Triggering Dive Animation");
    }
    public void SetStandAnimation()
    {
        agentAnimator.SetTrigger("stand");
         //Debug.Log("Triggering stand Animation");
    }

    public void SetTakeDamageAnimation()
    {
        agentAnimator.SetTrigger("Got hit");
        //Debug.Log("Triggering Hurt Animation");
    }

    public void SetDeathAnimation(bool val)
    {
        agentAnimator.SetBool("isDead", val);
        //Debug.Log("Triggering Death Animation");
    }

    /*public void SetReviveAnimation(bool val)
    {
        agentAnimator.SetBool("isDead", val);
        Debug.Log("Triggering Death Animation");
    }*/

    public void SetBuffAnimation(bool val)
    {
        agentAnimator.SetBool("isBuffing", val);
        //Debug.Log("Triggering Buff Animation");
    }

    public void SetSearchAnimation(bool val)
    {
        agentAnimator.SetBool("isSearching", val);
        //Debug.Log("Triggering Search Animation");
    }

     public void SetPanicAnimation(bool val)
    {
        agentAnimator.SetBool("isPanic", val);
        //Debug.Log("Triggering Panic Animation");
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void ShootAnimation() 
    {
        agentAnimator.SetTrigger("shoot");
    }

    public void EnableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}
