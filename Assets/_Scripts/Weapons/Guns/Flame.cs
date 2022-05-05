using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public GameObject fire;
    public float rotation = 0;
    public float rotationSpeed = 5;
    public float rotationDuration = 1;
    [SerializeField]
    float timeElapsed = 0;
    [SerializeField]
    float normalizedTime = 0;

    void Update()
    {
        RotateFire();
    }

    public void RotateFire()
    {
        Debug.Log("In Rotate Flame");
        timeElapsed += Time.deltaTime;
        normalizedTime = timeElapsed / rotationDuration;
        rotation = Mathf.Lerp(rotation, 360f, rotationSpeed * Time.deltaTime);

        fire.transform.localEulerAngles = new Vector3(rotation, 0, -90);

        if(timeElapsed >= rotationDuration || rotation >= 355f)
        {
            rotation = 0;
            timeElapsed = 0;
        }

    }
}
