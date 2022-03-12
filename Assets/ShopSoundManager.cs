using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ShopSoundManager : MonoBehaviour
{

    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

    [ParamRef]
    public string shop;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Shop.inShop){
            RuntimeManager.StudioSystem.setParameterByName(shop, 1);
        }
        else
            RuntimeManager.StudioSystem.setParameterByName(shop, 0);
    }

    void OnDestroy(){
        instance.release();
    }
}
