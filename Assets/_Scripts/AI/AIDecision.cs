using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIActionData aiActionData;
    protected AIMovementData aiMovementData;

    protected EnemyBrain enemyBrain;

    private void Awake()
    {
        aiActionData = transform.parent.parent.GetComponent<AIActionData>();
        aiMovementData = transform.parent.parent.GetComponent<AIMovementData>();
        enemyBrain = transform.parent.parent.parent.GetComponent<EnemyBrain>();
    }

    public abstract bool MakeADecision();
}
