using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BackgroundSoundManager : MonoBehaviour
{
    public CircleCollider2D detect;
    // Start is called before the first frame update
    void Start()
    {
        detect = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
 
    void OnCollisionEnter2D(Collider2D collider)
    {
        
    }
}
