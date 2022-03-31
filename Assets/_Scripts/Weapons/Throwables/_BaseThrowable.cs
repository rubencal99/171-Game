using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BaseThrowable : MonoBehaviour
{
    public float Speed;
    public Vector3 LaunchOffset;
    public bool Thrown = false;

    public int damage;

    Vector3[] pos;

    PlayerWeapon pw;

    int posCount = 0;

    // Start is called before the first frame update
    public void Start()
    {
        
        //var direction = transform.right + Vector3.up;
       // GetComponent<Rigidbody>().AddForce(direction * Speed, ForceMode.Impulse);
        //transform.Translate(LaunchOffset);
        pw = this.transform.parent.GetComponent<PlayerWeapon>();
        pos = pw.throwableArc.arcArray;
        //pos = this.transform.parent.GetComponent<PlayerWeapon>().throwableArc.arcArray;

        // Automatically destroy throwable after 5 seconds
       // Destroy(gameObject, 5);
    }

    public void Update()
    {
        // if(Thrown) {
        //     moveToPosition(pos[posCount]);
        //     if(transform.position.x >= pos[posCount].x)
        //         posCount++;
        // }
        transform.position += transform.position * pw.desiredAngle * Speed  * Time.deltaTime;

    }

    void moveToPosition(Vector3 targetPos) {
        var step = Speed * Time.deltaTime;
         transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    public void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.GetHit(damage, gameObject);
        }
        Destroy(gameObject);
    }
}
