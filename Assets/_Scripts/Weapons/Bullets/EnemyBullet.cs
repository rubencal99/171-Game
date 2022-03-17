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

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            bounce--;
            if(bounce < 0)
            {
                HitObstacle();
                StartCoroutine(destruction());
            }
            else
            {
                BounceBullet(collision);

            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HitEnemy(collision);
            // This check is for bullets that go through enemies like snipers or lasers etc.
            if (!BulletData.GoThroughHittable) {
                animator.Play("bulletdestructionenemy");
                 StartCoroutine(destruction());
            }
        }
        else
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), transform.GetComponent<Collider>());
        }
        
    }
}
