using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimations : MonoBehaviour
{
    protected Animator agentAnimator;

    private void Awake()
    {
        agentAnimator = GetComponent<Animator>();
    }

    public void SetWalkAnimation(bool val)
    {
        agentAnimator.SetBool("Walk", val);
    }

    public void SetTakeDamageAnimation()
    {
        agentAnimator.SetTrigger("Got hit");
        Debug.Log("Triggering Hurt Animation");
    }

    public void SetDeathAnimation(bool val)
    {
        agentAnimator.SetBool("IsDead", true);
        Debug.Log("Triggering Death Animation");
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }
}
