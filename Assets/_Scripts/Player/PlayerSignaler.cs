using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSignaler : object
{
    public static GameObject obj = GameObject.FindGameObjectWithTag("Player");
    public static Player Player = obj.GetComponent<Player>();
    public static PlayerPassives playerPassives = obj.GetComponent<PlayerPassives>();

    
    /*private static void Awake()
    {
        obj =  GameObject.FindGameObjectWithTag("Player");
        Player = obj.GetComponent<Player>();
        playerPassives = obj.GetComponent<PlayerPassives>();
    }*/

    public static float CallGunnerGloves(Gun gun)
    {
        if(PlayerAugmentations.AugmentationList["GunnerGloves"] == true)
        {
            return gun.weaponData.ReloadSpeed / gun.passives.ReloadMultiplier / PlayerAugmentations.GunnerGlovesSpeed;
        }
        return gun.weaponData.ReloadSpeed / gun.passives.ReloadMultiplier;
    }

    public static void CallBulletTime()
    {
        if(PlayerAugmentations.AugmentationList["BulletTime"] == true)
        {
            TimeManager.DoSlowMotion();
        }
    }

    public static void CallPlayerEpiBoost()
    {
        // Debug.Log("In Epi Boost");
        if(PlayerAugmentations.AugmentationList["Epinephrine"] == true)
        {
            Debug.Log("Epinephrine = true");
            Player.Heal(PlayerAugmentations.EpinephrineBoost);
        }
    }

    public static bool CallCasingRecycler(){
        if(PlayerAugmentations.AugmentationList["CasingRecycle"]){
             var recycleChance = Random.Range(0, PlayerAugmentations.CasingRecPer);
             if(recycleChance <= 15){
                 return true;
             }
             return false;
        }else{
            return false;
        }
    }
}
