using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSoundManager : MonoBehaviour
{
    
    public FMODUnity.EventReference shopBGMEvent;
    private FMOD.Studio.EventInstance shopBGMInst;
    // Start is called before the first frame update
    void Start()
    {
        //Fmod shit
        shopBGMInst = FMODUnity.RuntimeManager.CreateInstance(shopBGMEvent);
        shopBGMInst.start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Music starts");
        shopBGMInst.setParameterByName("InShop", 1);
    }

     void OnDestroy()
    {
        shopBGMInst.release();
    }
}
