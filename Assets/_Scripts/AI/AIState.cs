using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents general state of attached agent
// Stores references to AI Actions and Transitions
public class AIState : MonoBehaviour
{
    private EnemyBrain enemyBrain = null;
    [SerializeField]
    private List<AIAction> Actions = null;
    // Transitions should be ordered based on priority
    [SerializeField]
    private List<AITransition> Transitions = null;

    private void Awake()
    {
        enemyBrain = transform.root.GetComponent<EnemyBrain>();
    }

    // This will be called in enemy brain script
    // Depending on state, there'll be different actions/transitions
    public void UpdateState()
    {
        foreach (var action in Actions)
        {
            action.TakeAction();
        }
        foreach (var transition in Transitions)
        {
            // player in range == false -> Back to idle
            // player visible == true -> Chase
            bool result = false;
            foreach (var decision in transition.Decisions)
            {
                result = decision.MakeADecision();
                if (result == false)
                {
                    break;
                }
            }
            if (result)
            {
                if (transition.PositiveResult != null)
                {
                    enemyBrain.ChangetoState(transition.PositiveResult);
                    return;
                }
            }
            else
            {
                if (transition.NegativeResult != null)
                {
                    enemyBrain.ChangetoState(transition.NegativeResult);
                    return;
                }
            }
        }
    }
}
