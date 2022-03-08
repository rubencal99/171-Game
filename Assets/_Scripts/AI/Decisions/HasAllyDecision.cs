using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasAllyDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return HasAlly();
    }

    bool HasAlly(){
        var allies = GameObject.FindGameObjectsWithTag("Enemy");
        if(allies.Length > 1){
            return true;
        }
        enemyBrain.enemy.agentAnimations.SetBuffAnimation(false);
        return false;
    }
}
