using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimations : AgentAnimations
{
    public void SetCycloneAnimation(bool val)
    {
        agentAnimator.SetBool("Cyclone", val);
    }
}
