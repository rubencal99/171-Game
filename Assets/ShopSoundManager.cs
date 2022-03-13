using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ShopSoundManager : MonoBehaviour
{

    private FMOD.Studio.EventInstance ShopMusicInst;

    public FMODUnity.EventReference ShopMusicEvent;

    [ParamRef]
    public string shop;

    // Start is called before the first frame update
    void Start()
    {
        ShopMusicInst = FMODUnity.RuntimeManager.CreateInstance(ShopMusicEvent);
        ShopMusicInst.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        ShopMusicInst.start();
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
        ShopMusicInst.release();
    }
}
