using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[RequireComponent(typeof(LineRenderer))]

public class RenderThrowableArc : MonoBehaviour

{

    LineRenderer lr;



    public int arcHeight = 2;

    public float angle;

    public int resolution;



    float g; //force of gravity on the y axis

    float radianAngle;

    public float maxDistance = 0;

    public Vector3[] arcArray;

    GameObject[] dotArray;

    GameObject Player;

    public float xOffset, yOffset, zOffset = 0.0f;

    private async void Awake()

    {

        lr = GetComponent<LineRenderer>();

        g = Mathf.Abs(Physics.gravity.y);

        Player = this.transform.parent.gameObject;
        arcArray = new Vector3[resolution + 1];
        dotArray = new GameObject[resolution + 1];
        lr.positionCount = resolution + 1;

        for(int i = 1; i < resolution; i++) {
             dotArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
             dotArray[i].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
             dotArray[i].transform.parent = this.transform;
             dotArray[i].GetComponent<SphereCollider>().isTrigger = true;
            //. dotArray[i].s
        }

    }
    private void onDisable() {
        foreach(GameObject dot in dotArray) {
            Destroy(dot);
        }
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
        //lr.alignment = LineAlignment.Local;
      //  RenderArc();
    }

    void FixedUpdate() {
       // lr.SetPosition(0, Player.transform.localPosition);
      //  lr.SetPosition(resolution, Player.GetComponent<PlayerInput>().MousePos);
        float width =  lr.startWidth;
        lr.material.mainTextureScale = new Vector2(1f / width, 1.0f);
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

        lr.SetPositions(CalculateArcArray());
        lr.SetPosition(0,  Player.transform.localPosition);
        transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);

    }

    //Create an array of Vector 3 positions for the arc

    Vector3[] CalculateArcArray()

    {

        


       // radianAngle = Mathf.Deg2Rad * angle;
        radianAngle = Player.GetComponentInChildren<PlayerWeapon>().desiredAngle *  Mathf.Deg2Rad;

       // float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        maxDistance = Vector3.Distance(Player.transform.localPosition, Player.GetComponent<PlayerInput>().MousePos);
        Vector3 distanceVector = new Vector3(Player.GetComponent<PlayerInput>().MousePos.x -  Player.transform.localPosition.x,
                                                1,
                                                    Player.GetComponent<PlayerInput>().MousePos.z -  Player.transform.localPosition.z);
        Vector3 normalDistance = new Vector3(distanceVector.x / maxDistance, 1, distanceVector.z / maxDistance);
       
        for (int i = 1; i < resolution; i++)
        {

            float t = (float)i / (float)resolution;

            arcArray[i] = CalculateArcPoint(t, distanceVector);

            dotArray[i].transform.position = arcArray[i];

        }

        arcArray[resolution] = Player.GetComponent<PlayerInput>().MousePos;




        return arcArray;

    }



    private Vector3 CalculateArcPoint(float t, Vector3 normalDistance)

    {
        // Debug.Log("t =" + t + ", normal dist x =" +  normalDistance.x + ", normal dist z =" +  normalDistance.z);  
        float x = t * normalDistance.x;
        float z = t * normalDistance.z;
       // float y = t * ;

        //float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        x += Player.transform.position.x;
        z += Player.transform.position.z;

         float arc = arcHeight * (x - Player.transform.position.x) * (x - Player.GetComponent<PlayerInput>().MousePos.x) / (-0.25f * maxDistance * maxDistance);
        return new Vector3(x, arc, z);

    }



}