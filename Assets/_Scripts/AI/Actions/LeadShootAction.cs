using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadShootAction : AIAction
{
    public override void TakeAction()
    {
        var bulletSpeed = enemyBrain.Weapon.weaponData.BulletData.BulletSpeed;

        Vector3 TargetPosition = enemyBrain.Target.transform.position;
        // Time it'll take to reach player given bullet speed
        var dir = (TargetPosition - transform.position);
        var t = dir.magnitude / bulletSpeed;

        // Predict future position based on player velocity
        var futurePos = TargetPosition + (Vector3)enemyBrain.Target.GetComponent<PlayerMovement>().movementDirection;// * t;

        var aim = (futurePos - transform.position);
        Debug.DrawRay(enemyBrain.transform.position, aim);

        aiMovementData.PointerPosition = futurePos;
        enemyBrain.Aim(futurePos);
        enemyBrain.Attack();
    }
}
