using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends Bullet to give us access to Bullet SO
public class RegularBullet : Bullet
{
    // Need reference to our bullet's rigidbody
    protected Rigidbody rigidbody;

    protected float decay;

    protected Animator animator;
    protected int bounce;

    public override BulletDataSO BulletData
    {
        get => base.BulletData;
        set
        {
            base.BulletData = value;
            rigidbody = GetComponent<Rigidbody>();
            // drag is for bullets that slow down (ex. shotgun shells)
            rigidbody.drag = BulletData.Friction;
            // decay is for bullets that expire (melee)
            decay = BulletData.decayTime;
            bounce = BulletData.Bounce;
        }
    }

    public virtual void Start() {
        animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if(BulletData.hasDecay)
        {
            decay -= Time.deltaTime;
            if(decay <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void FixedUpdate()
    {
        if(rigidbody != null && BulletData != null)
        {
            // this moves our bullet in the direction that it is facing
            rigidbody.MovePosition(transform.position + BulletData.BulletSpeed * (Vector3)direction * Time.fixedDeltaTime);
            
        }
    }

    // There's a bug where the bullets collide with themselves if using multishot,
    // which is why Destroy() is inside the if instead of at the end on the func
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            /*bounce--;
            if(bounce < 0)
            {
                HitObstacle();
                StartCoroutine(destruction());
            }*/
            HitObstacle();
            StartCoroutine(destruction());
            /*else
            {
                Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
                Vector2 inNormal = collision.contacts[0].normal;
                Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
            }*/
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
            // This check is for bullets that go through enemies like snipers or lasers etc.
            if (!BulletData.GoThroughHittable) {
                animator.Play("bulletdestructionenemy");
                 StartCoroutine(destruction());
            }
        }
        
    }

    public void HitEnemy(Collider2D collision)
    {
        // This drops enemy health / destroys enemy
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(BulletData.Damage, gameObject);
        
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
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
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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

    public void BounceBullet(Collision2D collision)
    {
        //Vector2 inDirection = GetComponent<Rigidbody2D>().velocity;
        Vector2 inNormal = collision.contacts[0].normal;
        Vector2 newDirection = Vector2.Reflect(direction, inNormal);
        Debug.Log("In Bounce Bullet");
        Debug.Log("CURRENT Direction: " + direction);
        Debug.Log("New Direction: " + newDirection);
        Debug.Log("CURRENT Rotation: " + transform.rotation);
        /*if(newDirection.x != direction.x)
        {
            transform.Rotate(new Vector3(-transform.rotation.x, 0, 0));
        }
        else if(newDirection.y != direction.y)
        {
            transform.Rotate(new Vector3(0, -transform.rotation.y, 0));
        }*/
        Debug.Log("New Rotation: " + transform.rotation);
        direction = newDirection;
    }

    public void HitEnemy(Collision2D collision)
    {
        // This drops enemy health / destroys enemy
        var hittable = collision.gameObject.GetComponent<IHittable>();
        hittable?.GetHit(BulletData.Damage, gameObject);
    }

    public void HitObstacle()
    {
         // Debug.Log("Hitting obstacle");
        animator.Play("bulletdestructionobstacle");
    }

    public IEnumerator destruction() {

         yield return new WaitForSeconds(0.05f);
         Destroy(gameObject);
    }
}
