using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DroneSignaler : object
{
    public static void Update()
    {
        CallDroneArms();
        CallDroneBody();
        CallDroneLegs();
    }

    public static bool CallDroneHead()
    {
        if(PlayerAugmentations.AugmentationList["DroneHead"])
        {
            return true;
        }
        return false;
    }

    public static void CallDroneBody()
    {
        if(!PlayerAugmentations.AugmentationList["DroneBody"] && 
            Companion.instance.defaultWeapon.activeSelf == false)
            {
                Companion.instance.defaultWeapon.SetActive(true);
                Companion.instance.upgradedWeapon.SetActive(false);
            }
        else if(PlayerAugmentations.AugmentationList["DroneBody"] && 
            Companion.instance.upgradedWeapon.activeSelf == false)
            {
                Companion.instance.defaultWeapon.SetActive(false);
                Companion.instance.upgradedWeapon.SetActive(true);
            }
    }

    public static void CallDroneArms()
    {
        if(PlayerAugmentations.AugmentationList["DroneArms"])
        {
            Companion.instance.gameObject.GetComponent<PlayerPassives>().ROFMultiplier = Companion.instance.ROFMultiplier;
        }
        else
        {
            Companion.instance.gameObject.GetComponent<PlayerPassives>().ROFMultiplier = 1;
        }
    }

    public static void CallDroneLegs()
    {
        if(PlayerAugmentations.AugmentationList["DroneLegs"])
        {
            Companion.instance.gameObject.GetComponent<PlayerPassives>().SpeedMultiplier = Companion.instance.SpeedMultiplier;
        }
        else
        {
            Companion.instance.gameObject.GetComponent<PlayerPassives>().SpeedMultiplier = 1;
        }
    }
    
}
