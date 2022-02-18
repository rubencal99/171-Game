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
            transform.root.GetComponent<EnemySpanwer>().enabled = true;
        }
        return isEnraged;
    }
}
