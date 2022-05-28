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

    BoxCollider trigger;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mass = rigidbody.mass;
        isGrabbed = false;
        trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = new Vector3(1.0f, 1.0f, 1.0f);
    }

    /*void Update()
    {

    }*/
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(this.gameObject.transform.GetChild(0).tag == "key")
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            if(this.gameObject.transform.GetChild(0).tag == "key")
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void GrabObject()
    {
        isGrabbed = true;
        Player.instance.grabbing = true;
        Player.instance.grabbedObject = gameObject;
        offset = transform.position - Player.instance.transform.position;
         if(this.gameObject.transform.GetChild(0).tag == "key")
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
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
