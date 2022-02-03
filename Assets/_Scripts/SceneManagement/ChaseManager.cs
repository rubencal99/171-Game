using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseManager : MonoBehaviour
{
    bool chase = false;
    // List<LightManager> lights;
    LightManager[] lights;

    void Start(){
        lights = GameObject.FindObjectsOfType<LightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckChase();
    }

    private void CheckChase(){
        if(chase){
            foreach(LightManager light in lights){
                light.Chase = true;
            }
        }
        else{
            foreach(LightManager light in lights){
                light.Chase = false;
            }
        }
    }
}
