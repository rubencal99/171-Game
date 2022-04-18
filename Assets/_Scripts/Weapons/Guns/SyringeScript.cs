using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeScript : MonoBehaviour
{
    public EnemyBrain enemyBrain;
    EnemyGun enemyGun;
    
    void Awake()
    {
        enemyBrain = transform.parent.parent.parent.GetComponent<EnemyBrain>();
        enemyGun = transform.parent.GetComponent<EnemyGun>();
    }

    public void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Hitting something");
        if (collision.gameObject.tag == "Enemy")
        {   
            Debug.Log("Hit Enemy");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.isDying == true)
            {
                enemy.isDying = false;
                enemy.Health = 3;
                enemy.GetComponentInChildren<AgentAnimations>().SetWalkAnimation(true);
                enemy.DeadOrAlive();
                enemyBrain.Target = Player.instance.gameObject;
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            int damage = enemyGun.weaponData.BulletData.Damage;
            Player.instance.GetHit(damage, enemyGun.gameObject);
        }
    }
}
