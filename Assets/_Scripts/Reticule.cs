using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticule : MonoBehaviour
{
    
    private float minX = 0.0f;
    private float minY = 0.0f;
    private float maxX = Screen.width;
    private float maxY = Screen.height;
   
    public float bias = 0.65f;
    
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos =  Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, minX, maxX);
        mousePos.y = Mathf.Clamp(mousePos.y, minY, maxY);
        mousePos.z = /*Camera.main.nearClipPlane*/ 1.0f;
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        calculateMidPoint();
    }

    void calculateMidPoint() {
        Vector3 reticule = this.transform.position;
        Vector3 player = this.transform.parent.transform.position;
        Vector3 midpoint = this.transform.GetChild(0).position;

        bias = Mathf.Clamp(bias, 0.0f, 1.0f);
        midpoint = Vector3.Lerp(reticule, player, bias);
        // midpoint.x = reticule.x + (player.x - reticule.x) / 2.0f ;
        // midpoint.y = reticule.y + (player.y - reticule.y) / 2.0f ;
        midpoint.z = 1.0f ;

        this.transform.GetChild(0).position = midpoint;



    }
}
