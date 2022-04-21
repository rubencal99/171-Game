using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemyAction : AIAction
{
    public Companion companion;
    public float radius;
  public override void TakeAction()
  {
    if(companion.enemyTarget == null)
    {
        FindEnemy();
    }
  }

  public void FindEnemy()
  {
      Debug.Log("In find enemy");
    Enemy closestEnemy = null;
    float closestDistance = 999f;
    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
    foreach(Collider collider in colliders)
    {
        Enemy temp = collider.gameObject.GetComponent<Enemy>();
        if(temp)
        {
            float d = Vector3.Distance(collider.transform.position, transform.position);
            if(1 <= d && d < closestDistance)
            {
                closestEnemy = temp;
                closestDistance = d;
            }
        }
    }
    companion.enemyTarget = closestEnemy;
  }
}
