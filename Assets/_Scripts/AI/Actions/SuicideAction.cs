using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAction : AIAction
{
  public float delayTime;
  public float waitTime;
  public float radius;
  public LayerMask layerMask;
  public ParticleSystem ps;
  public GameObject avatar;
  public float damage;

  bool hasAttacked = false;

  public override void TakeAction()
  {
    // Here is where we trigger an explosion animation + effects
    //GetComponentInChildren<EnemyGrenade>().enabled = true;
    if(!hasAttacked)
    {
      hasAttacked = true;
      Debug.Log("In Suicide");
      StartCoroutine(Delay());
    }
    
  }

  public IEnumerator Delay()
  {
    Debug.Log("In Delay");
    enemyBrain.Attack();
    yield return new WaitForSeconds(delayTime);
    Debug.Log("After Delay");
    
    particles();
    splash();
    Invoke("WaitToDestroy", waitTime);
  }

  protected void splash() 
  {
    var inRange =  Physics.OverlapSphere(transform.position, radius, layerMask);
    foreach(var entity in inRange) {
        
        if(entity.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
          if(entity.GetComponent<Player>())
          {
            Debug.Log(entity.gameObject);
            entity.GetComponent<Player>().GetHit(damage);
          }
        }

            
            //HitEnemy(entity);
        else if(entity.gameObject.layer == LayerMask.NameToLayer("Enemy") && entity.gameObject != enemyBrain.gameObject)
          {
            Debug.Log(entity.gameObject);
            entity.GetComponent<Enemy>().GetHit(damage);
          }
    }
  }

  protected void particles() 
  {
    Debug.Log("In particles");
    var sh = ps.shape;
    sh.radius = radius;
    ps.Play();
    avatar.SetActive(false);
    transform.parent = null;
  }

  void WaitToDestroy()
  {
      Destroy(enemyBrain.gameObject);
  }
}
