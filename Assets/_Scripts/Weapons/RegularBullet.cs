using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends Bullet to give us access to Bullet SO
public class RegularBullet : Bullet
{
    // Need reference to our bullet's rigidbody
    protected Rigidbody2D rigidbody2D;

    public override BulletDataSO BulletData
    {
        get => base.BulletData;
        set
        {
            base.BulletData = value;
            rigidbody2D = GetComponent<Rigidbody2D>();
            // drag is for bullets that slow down (ex. shotgun shells)
            rigidbody2D.drag = BulletData.Friction;
        }
    }

    private void FixedUpdate()
    {
        if(rigidbody2D != null && BulletData != null)
        {
            // this moves our bullet in the direction that it is facing
            rigidbody2D.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }

    // There's a bug where the bullets collide with themselves if using multishot,
    // which is why Destroy() is inside the if instead of at the end on the func
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            HitObstacle();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
            Destroy(gameObject);
        }
        
    }

    private void HitEnemy(Collider2D collision)
    {
        // This drops enemy health / destroys enemy
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(BulletData.Damage, gameObject);
    }

    private void HitObstacle()
    {
        Debug.Log("Hitting obstacle");
    }
}
