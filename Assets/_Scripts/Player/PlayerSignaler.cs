using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSignaler : object
{
    public static GameObject obj = GameObject.FindGameObjectWithTag("Player");
    public static Player Player = obj.GetComponent<Player>();
    public static PlayerPassives playerPassives = obj.GetComponent<PlayerPassives>();

    public static PlayerWeapon playerWeapon = obj.GetComponentInChildren<PlayerWeapon>();

    public static bool usePredator = false;

    public static float predatorTimer = 0f;
    public static float predatorTotalTime = 3f;

     public static void Update(){
        if(usePredator){
            predatorTimer += Time.deltaTime;
        }
        if(predatorTimer >= predatorTotalTime){
            usePredator = false;
            predatorTimer = 0;
        }
     }
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
        if(PlayerAugmentations.AugmentationList["Epinephrine"] == true)
        {
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

    public static float CallSecondSkin(float damage){
        var curDamage = damage;
        if(PlayerAugmentations.AugmentationList["SecondSkin"]){
            return curDamage - curDamage * PlayerAugmentations.SkinAmount;
        }
        return curDamage;
    }

    public static float CallCheetahSpeed(){
        if(PlayerAugmentations.AugmentationList["CheetahSpeed"]){
            return PlayerAugmentations.CSAmount;
        }
        return 2000f;
        //return 1f;
    }

    public static bool CallPredatoryInstinct(){
        if(PlayerAugmentations.AugmentationList["PredatoryInstinct"]){
            return true;
        }
        return false;
    }

    public static float SetMovementSpeed(){
        float speedScalar = 1f;
        if(PlayerAugmentations.AugmentationList["CheetahSpeed"]){
            speedScalar += PlayerAugmentations.CSAmount;
        }if(usePredator){
            speedScalar += PlayerAugmentations.PredatoryAmount;
        }
        return speedScalar;
    }


}
