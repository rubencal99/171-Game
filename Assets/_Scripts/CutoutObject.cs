using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    [SerializeField]
    private Camera mainCamera;

    private void Awake()
    {
        
            mainCamera = transform.GetComponent<Camera>();
        
       // targetObject = this.transform;
    }

    private void Update()
    {
        RaycastHit hit;

        Vector3 offset = targetObject.position - transform.position;
        if(Physics.Raycast(transform.position, offset.normalized, out hit, Mathf.Infinity, wallMask)){
        
           if(hit.collider.transform.tag == "spheremask") {
               targetObject.transform.localScale = new Vector3(0f, 0f, 0f);
            //   Debug.Log("hit sphere");
           }
           else
           {
              //  Debug.Log("enabling cutout");
               targetObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
           }

        
        }
    }
}