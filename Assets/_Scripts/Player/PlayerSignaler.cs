using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSignaler : object
{
    public static GameObject obj = GameObject.FindGameObjectWithTag("Player");
    public static Player player  = obj.GetComponent<Player>();
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
            player.Heal(PlayerAugmentations.EpinephrineBoost);
        }
    }

    public static void CallHippoSkin(){ // work after saving file but breaks after 
        if(!PlayerAugmentations.hippoApplied && PlayerAugmentations.AugmentationList["HippoSkin"] == true ){
            PlayerAugmentations.hippoApplied = true;
            player.setMaxHp(player.MaxHealth + (int)(player.MaxHealth * 4 / 10));
            player.setMaxHp(player.MaxHealth);
        }
    }

    public static void CallWhiskers(){
        if(PlayerAugmentations.AugmentationList["Whiskers"] && !PlayerAugmentations.whiskerApplied){
            PlayerAugmentations.whiskerApplied = true;
        }
    }
}
