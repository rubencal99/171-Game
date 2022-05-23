using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Dictates what should happen when we enter a state
public abstract class AIAction : MonoBehaviour
{
    protected AIActionData aiActionData;
    protected AIMovementData aiMovementData;

    protected EnemyBrain enemyBrain;

    private void Awake()
    {
        aiActionData = transform.parent.parent.GetComponentInChildren<AIActionData>();
        aiMovementData = transform.parent.parent.GetComponentInChildren<AIMovementData>();
        enemyBrain = transform.parent.parent.GetComponent<EnemyBrain>();
    }

    public abstract void TakeAction();
}
