using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSignaler : object
{
    public static GameObject obj = GameObject.FindGameObjectWithTag("Player");
    public static Player Player = obj.GetComponent<Player>();
    public static PlayerPassives playerPassives = obj.GetComponent<PlayerPassives>();

    public static PlayerWeapon playerWeapon = obj.GetComponentInChildren<PlayerWeapon>();

    
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
             var recycleChance = Random.Range(0, 100);
             if(recycleChance <= PlayerAugmentations.CasingRecPer){
                 return true;
             }
             return false;
        }
        return false;
    }

    public static void CallWhiskers(){
        var mousepos = playerWeapon.pointerPos;
        var direction = mousepos - Player.transform.position;
        RaycastHit hit = new RaycastHit();
        var dist = PlayerAugmentations.whiskersDist;
        Physics.Raycast(Player.transform.position, direction, out hit, dist);
        if(hit.transform == null){
            //Player.transform.position += direction.normalized;
            Player.transform.position += direction.normalized * dist;
        }else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
            Debug.Log("Teleport into obstacle");
        }  
    }

    public static float CallDamageBuff(float damage){
        var curDamage = damage;
        if(PlayerAugmentations.AugmentationList["DamageBuff"]){
            return curDamage + curDamage * PlayerAugmentations.BuffAmount;
        }
        return curDamage;
    }
}
