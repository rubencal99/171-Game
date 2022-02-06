using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDeadAllyDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return HasDeadAlly();
    }

    bool HasDeadAlly(){
        var allies = GameObject.FindGameObjectsWithTag("Enemy");
        if(allies.Length > 1){
            foreach (GameObject ally in allies)
            {
                Enemy allyInfo = ally.GetComponent<Enemy>();
                if (allyInfo.isDying)
                {
                    enemyBrain.Target = ally;
                    return true;
                }
            }
        }
        return false;
    }
}
