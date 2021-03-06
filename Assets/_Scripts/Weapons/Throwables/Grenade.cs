using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : _BaseThrowable
{
    // Special calls for Grenade if we need them
     public float radius = 0.000025f;

    public float lifetime = 3.0f;
    bool isDetonated = false;

     public override void Update() {
        if(lifetime < 0.0f)
        {
            if(!Thrown && !isDetonated)
            {
                PlayerInput.instance.OnThrowButtonPressed?.Invoke();
            }
            isDetonated = true;
           destruction();
        }

        lifetime -= Time.fixedDeltaTime;

        if(!Thrown) {
            transform.position = transform.parent.parent.position;

        }
    }
   
    protected void destruction() {
         Vector3 location = transform.position;
            splash();
            Destroy(this.GetComponent<Rigidbody>());
            StartCoroutine(particles());
    }
    
    public virtual void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
           destruction();
        }

    }
  
    protected IEnumerator particles() {
             var sh = this.gameObject.GetComponentInChildren<ParticleSystem>().shape;
            sh.radius = radius;
            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }

    protected virtual void splash() {

       var inRange =  Physics.OverlapSphere(transform.position, radius);
       foreach(var entity in inRange) {
           if(entity.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                entity.GetComponent<Enemy>().GetHit(3);
                //HitEnemy(entity);
       }

      
    }

}
