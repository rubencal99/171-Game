using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    public float velocity;
    public float mass;
    public bool isGrabbed;
    Rigidbody rigidbody;
    public Vector3 offset;

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
        offset = transform.position - Player.instance.transform.position;
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
        //velocity = speed;
    }

    public void AdjustPosition()
    {
        transform.position = Player.instance.transform.position + offset;
    }
    
}
