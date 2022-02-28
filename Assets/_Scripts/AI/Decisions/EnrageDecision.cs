using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrageDecision : AIDecision
{
    public override bool MakeADecision()
    {
        bool isEnraged = transform.parent.GetComponent<EnrageAction>().Enraged;
        if (isEnraged)
        {
            transform.parent.parent.parent.GetComponent<EnemySpanwer>().enabled = true;
        }
        return isEnraged;
    }
}
