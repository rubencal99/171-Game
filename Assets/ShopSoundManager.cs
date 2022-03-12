using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSoundManager : MonoBehaviour
{

    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    // Start is called before the first frame update
    void Start()
    {
        //instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        //instance.start();
        FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Shop.inShop){
            //instance.setParameterByName("inShop", 1);
            FMODUnity.RuntimeManager.PlayOneShot(fmodEvent, transform.position);
        }
        else
            instance.setParameterByName("inShop", 0);
        */
    }

    void OnDestroy(){
        //instance.release();
    }
}
