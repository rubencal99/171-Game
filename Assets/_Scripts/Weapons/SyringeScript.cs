using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hitting something");
        if (collision.gameObject.tag == "Enemy")
        {   
            Debug.Log("Hit Enemy");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.isDying == true)
            {
                enemy.isDying = false;
                enemy.Health = 3;
                enemy.DeadOrAlive();
            }
        }
    }
}
