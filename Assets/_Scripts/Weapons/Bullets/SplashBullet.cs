using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We only need this class to take care of trail rendering
public class SplashBullet : RegularBullet
{
    float lifetime;
    [Range(1,10)]
    public float radius;

    void Awake(){
        lifetime = BulletData.decayTime;
    }

    void Update() {
       // if(lifetime < 0.0f)
       //     splash();//Destroy(gameObject);
        
      //  lifetime -= Time.fixedDeltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") ||
                collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {

            Vector3 location = transform.position;
            splash();
            Destroy(this.GetComponent<Rigidbody2D>());
            StartCoroutine(particles());
        }

      
    }

    private IEnumerator particles() {
             var sh = GetComponent<ParticleSystem>().shape;
            sh.radius = radius / 4.0f;
            GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
    }

    private void splash() {

       var inRange =  Physics2D.OverlapCircleAll(transform.position, radius);
       foreach(var entity in inRange) {
           if(entity.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                HitEnemy(entity);
       }

      
    }

}
