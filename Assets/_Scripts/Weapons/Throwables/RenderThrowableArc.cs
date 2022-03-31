using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[RequireComponent(typeof(LineRenderer))]

public class RenderThrowableArc : MonoBehaviour

{

    LineRenderer lr;



    public float velocity;

    public float angle;

    public int resolution;



    float g; //force of gravity on the y axis

    float radianAngle;



    private void Awake()

    {

        lr = GetComponent<LineRenderer>();

        g = Mathf.Abs(Physics.gravity.y);

    }



    private void OnValidate()

    {

        if(lr!=null && Application.isPlaying){

            RenderArc();

        }

    }



    // Start is called before the first frame update

    void Start()

    {
       // this.gameObject.transform.localPosition = this.transform.parent.position;
        lr.alignment = LineAlignment.Local;
        RenderArc();

    }

    public void SetArcAngle()
    {
      //  this.transform.localRotation = this.transform.parent.GetComponentInChildren<PlayerWeapon>().rotation;
      //  this.transform.Rotate(45f, 0, 0);
       //  RenderArc();
    }



    //initialization

    void RenderArc()

    {

        // obsolete: lr.SetVertexCount(resolution + 1);

        lr.positionCount = resolution + 1;

        lr.SetPositions(CalculateArcArray());
       // lr.SetPosition(0,  this.transform.parent.localPosition);

    }

    //Create an array of Vector 3 positions for the arc

    Vector3[] CalculateArcArray()

    {

        Vector3[] arcArray = new Vector3[resolution + 1];



        radianAngle = Mathf.Deg2Rad * angle;

        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        //float maxDistance = Vector3.Distance(this.transform.parent.GetComponentInChildren<PlayerWeapon>().pointerPos, this.transform.localPosition);
        Debug.Log("arc distance = " + maxDistance);

        for (int i = 1; i <= resolution; i++)

        {

            float t = (float)i / (float)resolution;

            arcArray[i] = CalculateArcPoint(t, maxDistance);

        }

        return arcArray;

    }



    Vector3 CalculateArcPoint(float t, float maxDistance)

    {

        float x = t * maxDistance;

        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        x += this.transform.parent.localPosition.x;
        y += this.transform.parent.localPosition.z;
        return new Vector3(x, 1, y);

    }



}