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
        if(!mainCamera)
        {
            mainCamera = transform.root.parent.GetComponent<Camera>();
        }
        
        targetObject = this.transform;
    }

    private void Update()
    {
        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
           if(hitObjects[i].transform.tag == "spheremask") {
               targetObject.transform.localScale = new Vector3(0f, 0f, 0f);
           }
           else
           {
               targetObject.transform.localScale = new Vector3(1f, 1f, 1f);
           }

        }
    }
}