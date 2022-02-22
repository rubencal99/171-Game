using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadAimAction : AIAction
{
    public override void TakeAction()
    {
        var bulletSpeed = enemyBrain.Weapon.weaponData.BulletData.BulletSpeed;

        Vector3 TargetPosition = enemyBrain.Target.transform.position;
        // Time it'll take to reach player given bullet speed
        var t = (TargetPosition - transform.position).magnitude / bulletSpeed;

        // Predict future position based on player velocity
        var futurePos = TargetPosition + (Vector3)enemyBrain.Target.GetComponent<PlayerMovement>().movementDirection * t;

        var aim = (futurePos - transform.position).normalized;

        // aiMovementData.PointOfInterest = enemyBrain.Target.transform.position;
        enemyBrain.Aim(aim);
    }
}
