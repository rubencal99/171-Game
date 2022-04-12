using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BaseThrowable : MonoBehaviour
{
    float Speed;

    public float SpeedBonus = 1.0f;
    public Vector3 LaunchOffset;
    public bool Thrown = false;

    public int damage;

    Vector3[] pos;

    Vector3[] targetPos;

    PlayerWeapon pw;

    int posCount = 0;

    // Start is called before the first frame update
    public async void Start()
    {
         pw = this.transform.parent.GetComponent<PlayerWeapon>();
        //transform.Translate(LaunchOffset);
       
        pos = (Vector3[])pw.throwableArc.arcArray.Clone();
        targetPos = new Vector3[pos.Length];
        Speed = pw.throwableArc.maxDistance;
        for(int i = 0; i < pos.Length; i++) {
            targetPos[i] = pos[i];
        }
        //pos = this.transform.parent.GetComponent<PlayerWeapon>().throwableArc.arcArray;

        // Automatically destroy throwable after 5 seconds
        //Destroy(gameObject, 5);
    }

    public virtual void Update()
     {
    //     if(Thrown && posCount < targetPos.Length) {
    //         moveToPosition(targetPos[posCount]);
    //         if(transform.position.x == pos[posCount].x && transform.position.z == pos[posCount].z)
    //             posCount++;
    //     }
      //  transform.position += transform.position * pw.desiredAngle * Speed  * Time.deltaTime;
        if(!Thrown) {
            transform.position = transform.parent.parent.position;
        }
    }

    public void addForce() {
          var direction = pw.aimDirection + Vector3.up;
         GetComponent<Rigidbody>().useGravity = true;
         GetComponent<Rigidbody>().AddForce(direction * (Speed + SpeedBonus), ForceMode.Impulse);
         transform.parent = null;
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
            Destroy(gameObject);
        }
       
    }
}
