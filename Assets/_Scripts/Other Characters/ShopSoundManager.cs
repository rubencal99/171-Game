using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ShopSoundManager : MonoBehaviour
{

    private FMOD.Studio.EventInstance ShopMusicInst;

    public FMODUnity.EventReference ShopMusicEvent;
    public FMODUnity.EventReference ShopGreeting;

    [ParamRef]
    public string shop;
    private bool Greeted = false;
    public Shop shopInst;

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
        if(shopInst.inShop){
            RuntimeManager.StudioSystem.setParameterByName(shop, 1);
            if (!Greeted){
                FMODUnity.RuntimeManager.PlayOneShot(ShopGreeting); 
                Greeted = true;
            }
            
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(shop, 0);
            Greeted = false;
        }  
        // Debug.Log("greeting = " + Greeted);
    }

    void OnDestroy(){
        ShopMusicInst.release();
    }
}
