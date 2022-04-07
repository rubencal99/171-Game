using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    public float floatPoint;

    public float amplitude;
    public float period;
    public float speed;
    float timer;
    public CapsuleCollider capsule;
    public AgentRenderer agentRenderer;
    public float center;
    public Vector3 spritePos;
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = period;
        capsule = GetComponent<CapsuleCollider>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = period;
            ToggleAmplitude();
        }

        /*var z = Mathf.Lerp(transform.position.z, transform.position.z + amplitude, speed);
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, z);
        transform.position = pos;*/

        center = Mathf.Lerp(capsule.center.y, amplitude, speed);
        capsule.center = new Vector3(0, center, 0);
        spritePos = new Vector3(transform.position.x, transform.position.y, transform.position.z + center);
        agentRenderer.transform.position = spritePos;
        //var y = Mathf.Lerp(agentRenderer.transform.position.y, agentRenderer.transform.position.y + capsule.center.y, speed);
        //agentRenderer.transform.position.z + capsule.center.y
        //agentRenderer.transform.position = new Vector3(agentRenderer.transform.position.x, agentRenderer.transform.position.y, y);
        
    }

    void ToggleAmplitude()
    {
        amplitude = -amplitude;
    }
}
