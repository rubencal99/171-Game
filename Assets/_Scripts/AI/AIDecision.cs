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
        aiActionData = transform.parent.parent.parent.GetComponentInChildren<AIActionData>();
        aiMovementData = transform.parent.parent.parent.GetComponentInChildren<AIMovementData>();
        enemyBrain = transform.parent.parent.parent.GetComponent<EnemyBrain>();
    }

    public abstract bool MakeADecision();
}
