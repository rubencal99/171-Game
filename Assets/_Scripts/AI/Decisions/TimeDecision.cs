using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDecision : AIDecision
{
    public float Timer = 4f;
    public float time = 4f;
    public override bool MakeADecision()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = Timer;
            return true;
        }
        return false;
    }


}
