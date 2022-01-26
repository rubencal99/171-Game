using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BaseThrowable : MonoBehaviour
{
    public float Speed;
    public Vector3 LaunchOffset;
    public bool Thrown;

    public int damage;

    // Start is called before the first frame update
    public void Start()
    {
        var direction = transform.right + Vector3.up;
        GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse);
        transform.Translate(LaunchOffset);

        // Automatically destroy throwable after 5 seconds
        Destroy(gameObject, 5);
    }

    public void Update()
    {
        transform.position += -transform.right * Speed  * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.GetHit(damage, gameObject);
        }
        Destroy(gameObject);
    }
}
