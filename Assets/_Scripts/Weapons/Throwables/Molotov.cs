using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : _BaseThrowable
{
    // Special calls for Grenade if we need them
     public float radius = 0.000025f;

     public float LingerTime = 4.0f;

    public float lifetime = 3.0f;
     public override void Update() {
        if(lifetime < 0.0f)
           destruction();
        
        lifetime -= Time.fixedDeltaTime;

        if(!Thrown) {
            transform.position = transform.parent.parent.position;

        }
    }
   
    private void destruction() {
         Vector3 location = transform.position;
            StartCoroutine(splash());
            Destroy(this.GetComponent<Rigidbody>());
            StartCoroutine(particles());
    }
    
    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
           destruction();
        }

    }
  
    private IEnumerator particles() {
             var sh = this.gameObject.GetComponentInChildren<ParticleSystem>().shape;
            sh.radius = radius;
            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }

    private IEnumerator splash() {

       var inRange =  Physics.OverlapSphere(transform.position, radius);
       foreach(var entity in inRange) {
           if(entity.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                entity.GetComponent<Enemy>().GetHit(3);
                //HitEnemy(entity);
       }
      yield return new WaitForSeconds(LingerTime);

      
    }

}
