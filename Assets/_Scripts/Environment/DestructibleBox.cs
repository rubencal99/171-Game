using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : Enemy
{
    // Start is called before the first frame update

    public bool isDestructible = true;
    public void  Start()
    {
        Health = 3;
    }

    // Update is called once per frame
    public override void GetHit(float damage, GameObject damageDealer) {
        var agent = this.GetComponent<AgentMovement>();
        
        if(Health > 0 && isDestructible) {
            Health -= 1;
             OnGetHit?.Invoke();
        if(damageDealer.GetComponent<Bullet>())
        {
            BulletDataSO bulletData = damageDealer.GetComponent<Bullet>().BulletData;
            agent.Knockback(bulletData.KnockbackDuration, bulletData.KnockbackPower / 2f, -damageDealer.GetComponent<Bullet>().direction);
        }
        else if(damageDealer.GetComponent<Melee>())
        {
            MeleeDataSO meleeData = damageDealer.GetComponent<Melee>().meleeData;
            var weaponPosition = damageDealer.transform.parent.position;
            var direction = transform.position - weaponPosition;
            agent.Knockback(meleeData.KnockbackDelay, meleeData.KnockbackPower / 2f, -direction);
        }
        }
        else if(isDestructible) {
            OnDie?.Invoke();
            Die();
        }
    }
    
    void Update()
    {
        
    }
}
