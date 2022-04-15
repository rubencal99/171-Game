using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDecision : AIDecision
{
    [field: SerializeField]
    [field: Range(10, 100)]
    public int healthThreshold { get; set; } = 50;

    public override bool MakeADecision()
    {
        float health = transform.parent.parent.parent.GetComponent<Enemy>().Health;
        if (health < healthThreshold)
        {
            return true;
        }
        return false;
    }
}
