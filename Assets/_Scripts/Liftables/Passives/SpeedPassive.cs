using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPassive : _BasePassive
{
    [SerializeField]
    protected float multiplier;

    public override IEnumerator Pickup(Collider2D player)
    {
        MovementDataSO movementData = player.GetComponent<AgentMovement>().MovementData;
        movementData.maxSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        movementData.maxSpeed /= multiplier;

        Destroy(gameObject);
    }
}
