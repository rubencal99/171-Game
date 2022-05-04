using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{
    
    private float minX = -40f;
    private float minZ = -40f;
    private float maxX = Screen.width;
    private float maxZ = Screen.height;
   
    public float bias = 0.65f;

    PlayerInput playerInput;

    /*void Awake()
    {
        playerInput = transform.parent.GetComponent<PlayerInput>();
        if(!Camera)
        {
            Camera = CameraShake.Instance.gameObject;
        }
        
    }*/

    void Start()
    {
        playerInput = transform.parent.GetComponent<PlayerInput>();
        if(!Camera)
        {
            Camera = CameraShake.Instance.gameObject;
        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =  playerInput.MousePos;
        mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
        mousePos.z = Mathf.Clamp(mousePos.z, minZ, maxZ);
        mousePos.y = /*Camera.main.nearClipPlane*/ 0;
        //this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = mousePos;
        calculateMidPoint();
        //transform.LookAt(Camera.transform);
    }

    protected void LateUpdate()
    {
        //transform.LookAt(Camera.transform.position);
        //transform.rotation = Quaternion.LookRotation(transform.position - Camera.transform.position, transform.up);
    }

    void calculateMidPoint() {
        Vector3 reticule = this.transform.position;
        Vector3 player = this.transform.parent.transform.position;
        Vector3 midpoint = this.transform.GetChild(0).position;

        bias = Mathf.Clamp(bias, 0.0f, 1.0f);
        midpoint = Vector3.Lerp(reticule, player, bias);
        // midpoint.x = reticule.x + (player.x - reticule.x) / 2.0f ;
        // midpoint.y = reticule.y + (player.y - reticule.y) / 2.0f ;
        midpoint.y = 0;

        this.transform.GetChild(0).position = midpoint;



    }
}
