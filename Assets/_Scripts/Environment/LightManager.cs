using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//**********************************
//**********************************
// IMPORTANT:
// If runtime issues are being experienced, it might be because all lights are being updated at runtime
// If this happens, we'll want to let go of the smooth transitions from Mathf.Lerp
//**********************************
//**********************************
[RequireComponent(typeof(CircleCollider2D))]
public class LightManager : MonoBehaviour
{
    protected Light2D lightSource;
    protected CircleCollider2D trigger;

    //LightDataSO needed for the info on the light
    [field: SerializeField]
    public LightDataSO LightData {get; set;}

    [SerializeField]
    protected float lightIntensity;
    [SerializeField]
    protected float lightLevel;
    [SerializeField]
    protected float innerRadius;
    [SerializeField]
    protected float outerRadius;
    [SerializeField]
    protected bool indoor;
    public bool chase;
    //[field: SerializeField]
    public bool Chase{
        get {return chase;}
        set{
            Debug.Log("In Set Chase");
            if (chase == value) return;
            Debug.Log("chase = " + chase);
            chase = value;
            Debug.Log("chase cahnged to " + chase);
            SetChase(chase);
        }
    }
    [SerializeField]
    float smooth;


    [SerializeField]
    protected float triggerRadius;

    private void Start(){
        if (LightData){
            lightIntensity = LightData.Intensity;
            lightLevel = LightData.LightLevel;
            innerRadius = LightData.innerRadius;
            outerRadius = LightData.outerRadius;
            indoor = LightData.indoor;
            chase = LightData.chase;
            triggerRadius = outerRadius - 2;
            smooth = 2;
        }
        lightSource = GetComponentInChildren<Light2D>();
        trigger = GetComponent<CircleCollider2D>();
        trigger.radius = triggerRadius;
        SetLight();
    }

    private void Update(){
        SetChase(Chase);
    }

    private void SetChase(bool value){
        if(indoor){
            if(value){
                // Debug.Log("Red Mode!");
                lightSource.color = Color.Lerp(lightSource.color, Color.red, smooth * Time.deltaTime); //smooth * Time.deltaTime);
                lightLevel = 2;
            }
            else{
                lightSource.color = Color.Lerp(lightSource.color, Color.yellow, smooth * Time.deltaTime); //smooth * Time.deltaTime);
                lightLevel = 1;
            }
        }
        SetLight();
    }

    // We ideally want to use MAthf.Lerp to smoothly transition between states
    // similar to the Color.Lerp transition
    private void SetLight(){
        lightSource.intensity = lightIntensity * lightLevel; // Mathf.Lerp(lightIntensity, lightIntensity * lightLevel, 0.05f); //smooth*Time.deltaTime);
        lightSource.pointLightInnerRadius = innerRadius * lightLevel; // Mathf.Lerp(innerRadius, innerRadius * lightLevel, 0.05f); //smooth*Time.deltaTime);
        lightSource.pointLightOuterRadius = outerRadius * lightLevel; // Mathf.Lerp(outerRadius, outerRadius * lightLevel, 0.05f); //smooth*Time.deltaTime);
        triggerRadius = lightSource.pointLightOuterRadius - 2;
        trigger.radius = triggerRadius; // Mathf.Lerp(trigger.radius, triggerRadius, 0.05f); //smooth*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Chase = true;
        }
     }
     
     void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Chase = false;
        }
     }
}
