using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenade : Grenade
{
    public override void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
           destruction();
        }

    }

    protected override void splash() {

        var inRange =  Physics.OverlapSphere(transform.position, radius);
        foreach(var entity in inRange) {
            if(entity.gameObject.layer == LayerMask.NameToLayer("Player"))
                entity.GetComponent<Player>().GetHit(damage, gameObject);
                //HitEnemy(entity);
            else if(entity.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                entity.GetComponent<Enemy>().GetHit(damage, gameObject);
       }

      
    }

}
