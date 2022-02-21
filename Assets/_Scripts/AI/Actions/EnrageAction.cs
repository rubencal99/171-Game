using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrageAction : AIAction
{

    public bool Enraged = false;
    [SerializeField]
    public AIState chase;
    public AIState eChase;

    public float enrageTimer;
    public override void TakeAction()
    {
        PlayerPassives passives = transform.parent.parent.GetComponent<PlayerPassives>();
        passives.SpeedMultiplier = 1.3f;
        aiMovementData.Direction = Vector2.zero;
        // aiMovementData.PointOfInterest = transform.position;
        enemyBrain.Move(aiMovementData.Direction);
        enemyBrain.Aim(aiMovementData.PointOfInterest);
        chase.GetComponent<StarChaseAction>().enabled = false;
        StartCoroutine(Enrage());
    }

    private IEnumerator Enrage()
    {
        var agentRenderer = transform.root.GetComponentInChildren<AgentRenderer>();
        agentRenderer.isEnraged = true;
        yield return new WaitForSeconds(3f);
        Enraged = true;
        eChase.GetComponent<StarChaseAction>().enabled = true;
    }
}
