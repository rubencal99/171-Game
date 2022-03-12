using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : RegularBullet
{
    // There's a bug where the bullets collide with themselves if using multishot,
    // which is why Destroy() is inside the if instead of at the end on the func
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            HitObstacle();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Debug.Log("Hitting player!");
            HitEnemy(collision);
            Destroy(gameObject);
        }
        
    }
}
