using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSignaler : object
{
    public static GameObject obj = GameObject.FindGameObjectWithTag("Player");
    public static Player Player = Player.instance;
    public static PlayerPassives playerPassives = obj.GetComponent<PlayerPassives>();

    public static PlayerWeapon playerWeapon = obj.GetComponentInChildren<PlayerWeapon>();

    public static bool usePredator = false;

    public static float predatorTimer = 0f;
    public static float predatorTotalTime = 3f;

    
    /*private static void Awake()
    {
        obj =  GameObject.FindGameObjectWithTag("Player");
        Player = obj.GetComponent<Player>();
        playerPassives = obj.GetComponent<PlayerPassives>();
    }*/

    public static void Update()
    {
        CheckWhiskers();
        CallDrone();
    }
    
    public static void SetSignaler()
    {
        Player = Player.instance;
        obj = Player.gameObject;
        playerPassives = obj.GetComponent<PlayerPassives>();
        playerWeapon = obj.GetComponentInChildren<PlayerWeapon>();
    }

    public static float CallGunnerGloves(Gun gun)
    {
        if(PlayerAugmentations.AugmentationList["GunnerGloves"] == true)
        {
            Debug.Log("In Gunner Gloves");
            Debug.Log("Reload Speed was " + gun.weaponData.ReloadSpeed);
            Debug.Log("Reload Speed now " + gun.weaponData.ReloadSpeed / PlayerAugmentations.GunnerGlovesSpeed);
            return gun.weaponData.ReloadSpeed / gun.passives.ReloadMultiplier / PlayerAugmentations.GunnerGlovesSpeed;
        }
        return gun.weaponData.ReloadSpeed / gun.passives.ReloadMultiplier;
    }

    public static void CallBulletTime()
    {
        if(PlayerAugmentations.AugmentationList["BulletTime"] == true)
        {
            TimeManager.DoSlowMotion(PlayerAugmentations.BulletTT);
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
            //Debug.Log("In casing recycle");
            var recycleChance = Random.Range(0, 100);
            //Debug.Log("Recycle percent = " + recycleChance);
            if(recycleChance <= (PlayerAugmentations.CasingRecPer * RecBuff())){
                return true;
            }
            return false;
        }
        if(PlayerAugmentations.AugmentationList["DoomSlayer"]){
            //Debug.Log("In casing recycle");
            var recycleChance = Random.Range(0, 100);
            //Debug.Log("Recycle percent = " + recycleChance);
            if(recycleChance <= (PlayerAugmentations.DoomRecycle * RecBuff())){
                return true;
            }
            return false;
        }
        return false;
    }

    public static void CallWhiskers(){
        PlayerAugmentations.inWhiskers = true;
        var mousepos = playerWeapon.pointerPos;
        var direction = mousepos - Player.transform.position;
        direction.y = 0;
        RaycastHit hit = new RaycastHit();
        var dist = PlayerAugmentations.whiskersDist;
        Physics.Raycast(Player.transform.position, direction, out hit, dist);
        if(hit.transform == null){
            //Player.transform.position += direction.normalized;
            Player.transform.position += direction.normalized * dist;
        }else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
            Debug.Log("About to TP into obstacle");
            Debug.Log("Current Position: " + Player.transform.position);
            Debug.Log("Hit Position: " + hit.transform.position);
            var point = hit.transform.position - direction.normalized * 2;
            point.y = Player.transform.position.y;
            Debug.Log("TP Point: " + point);
            Player.transform.position = point;
        }  
    }

    public static void CheckWhiskers()
    {
        if(PlayerAugmentations.inWhiskers)
        {
            PlayerAugmentations.whiskersTimer -= Time.deltaTime;
            if(PlayerAugmentations.whiskersTimer <= 0)
            {
                PlayerAugmentations.inWhiskers = false;
                PlayerAugmentations.whiskersTimer = PlayerAugmentations.whiskersTime;
            }
        }
    }

    public static float CallDamageBuff(float damage){
        var curDamage = damage;
        if(PlayerAugmentations.AugmentationList["DamageBuff"]){
            return curDamage + curDamage * (PlayerAugmentations.BuffAmount * BuffDamBuff());
        }
        ////////////////////////////////Doom Buff///////////////////////
        if(PlayerAugmentations.AugmentationList["DoomSlayer"]){
            return curDamage + curDamage * (PlayerAugmentations.DoomBuff * BuffDamBuff());
        }
        return curDamage;
    }

    public static float CallSecondSkin(float damage){
        var curDamage = damage;
        if(PlayerAugmentations.AugmentationList["SecondSkin"]){
            return curDamage - curDamage * (PlayerAugmentations.SkinAmount * SkinBuff());
        }
        ////////////////////////////////Doom Half Damage///////////////////////
        if(PlayerAugmentations.AugmentationList["DoomSlayer"]){
            return curDamage + curDamage * (PlayerAugmentations.DoomHalfDam * SkinBuff());
        }
        return curDamage;
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

    public static void CheckPredator(){
        if(PlayerSignaler.usePredator){
           PlayerSignaler.predatorTimer  += Time.deltaTime;
        }
        if(PlayerSignaler.predatorTimer  >= PlayerSignaler.predatorTotalTime){
            PlayerSignaler.usePredator = false;
            PlayerSignaler.predatorTimer  = 0;
        }
    }

    public static void CallDrone()
    {
        if(PlayerAugmentations.AugmentationList["Drone"] && Player.instance.Drone == null)
        {
            Player.instance.InstantiateDrone();
        }
        else if(!PlayerAugmentations.AugmentationList["Drone"] && Player.instance.Drone)
        {
            Player.instance.DestroyDrone();
        }
    }
    public static float CallElephantStrength(){
        float strength = 1f;
        if(PlayerAugmentations.AugmentationList["ElephantStrength"]){
            strength = PlayerAugmentations.EStrength;
        }
        return strength;
    }

    public static bool CallAngelsGrace(){
        if(PlayerAugmentations.AugmentationList["AngelsGrace"]){
            return true;
        }
        return false;
    }

    public static float BuffHippo(){
        if(PlayerAugmentations.AugmentationList["HungryHippo"]){
            return PlayerAugmentations.HippoBuff;
        }
        return 0;
    }

    public static float BuffDamBuff(){
        if(PlayerAugmentations.AugmentationList["xXx"]){
            return PlayerAugmentations.xXxBuff;
        }
        return 1;
    }

    public static float SkinBuff(){
        if(PlayerAugmentations.AugmentationList["MetalSkin"]){
            return PlayerAugmentations.MetalAmount;
        }
        return 1f;
    }

    public static int RecBuff(){
        if(PlayerAugmentations.AugmentationList["CaptainPlanet"]){
            return PlayerAugmentations.CapRecycle;
        }
        return 1;

    }

    public static int CallDoubleMag(){
        if(PlayerAugmentations.AugmentationList["DoubleMag"]){
            return  PlayerAugmentations.MagAmount;
        }
        return 1;
    }

    public static float CallQuickdraw()
    {
        if(PlayerAugmentations.AugmentationList["Quickdraw"])
        {
            return PlayerAugmentations.DrawTime;
        }
        return 1;
    }

    public static bool CallDoubleShot(){
        if(PlayerAugmentations.AugmentationList["DoubleShot"]){
            return true;
        }
        return false;
    }
    public static float CallTriggerHappy()
    {
        if(PlayerAugmentations.AugmentationList["TriggerHappy"])
        {
            return PlayerAugmentations.TriggerMultiplier;
        }
        return 1;
    }
}
