using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public float mass;
    public bool isGrabbed;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mass = rigidbody.mass;
        isGrabbed = false;
    }

    /*void Update()
    {

    }*/

    public void GrabObject()
    {
        isGrabbed = true;
        Player.instance.grabbing = true;
        Player.instance.grabbedObject = gameObject;
    }

    public void LetGoObject()
    {
        isGrabbed = false;
        Player.instance.grabbing = false;
        Player.instance.grabbedObject = null;
    }

    public void AdjustSpeed(Vector3 speed)
    {
        rigidbody.velocity = speed;
    }
    
}
