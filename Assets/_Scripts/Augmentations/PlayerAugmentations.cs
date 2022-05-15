using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerAugmentations : object
{
    public static ItemInventory Ii;

    [SerializeField]
    //////////////////////////////EPINEPHRINE////////////////////////////////////
    public static bool Epinephrine = false;
    public static float EpinephrineBoost = 1f;
    //////////////////////////////BULLETTIME/////////////////////////////////////
    public static bool BulletTime = false;
    public static float BulletTT = 0.5f;
    public static float BulletTimeIntensity = 0.5f;

    //////////////////////////////GUNNERGLOVE/////////////////////////////////////
    public static bool  GunnerGloves = false;
    public static float GunnerGlovesSpeed = 5f;

    //////////////////////////////DEFLECTIONSHIELD////////////////////////////////
    public static bool DeflectionShield = false;
    public static float DefelctionTime = 3f;//this could change

    //////////////////////////////HIPPOSKIN////////////////////////////////////////
    public static bool HippoSkin = false;
    public static bool HippoApplied = false;
    public static float HippoHealth = 5f;

    //////////////////////////////CASINGRECYCLE/////////////////////////////////////
    public static bool CasingRecycle = false;
    public static int CasingRecPer = 15; 

    //////////////////////////////WHISKERS//////////////////////////////////////////
    public static bool Whiskers = false;
    public static bool inWhiskers = false;
    public static float whiskersDist = 5f;
    public static float whiskersTime = 2f;
    public static float whiskersTimer = 2f;

    //////////////////////////////HOOKSHOT//////////////////////////////////////////
    public static bool HookShot = false;
    //////////////////////////////AutoDoc/////////////////////////////////////
    public static bool AutoDoc = false;
    public static float AutoDocHeal = 0.05f;
    public static float AutoDocCoolDown = 10f;
    public static bool AutoDocUsed = false;

    //////////////////////////////Drone////////////////////////////////////////
    public static bool Drone = false;
    public static bool DroneHead = false;
    public static bool DroneArms = false;
    public static bool DroneBody = false;
    public static bool DroneLegs = false;

    //////////////////////////////Thorns////////////////////////////////////////
    public static bool Thorns = false;
    public static float ThornDam = 2f;

    public static float ThornKO = 0.1f;
    public static float ThornPushAmount = 3f;

    //////////////////////////////DamageBuff/////////////////////////////////////
    public static bool DamageBuff = false;
    public static float BuffAmount = 0.25f;
    //////////////////////////////CheetahSpeed////////////////////////////////////////
    public static bool CheetahSpeed = false;
    public static float CSAmount = 0.2f;
    //////////////////////////////Predator////////////////////////////////////////
    public static bool Predator = false;
    public static float PredatoryAmount = 0.4f;
    public static float predatorTimer = 0f;
    public static float predatorTotalTime = 3f;
    //////////////////////////////SecondSkin////////////////////////////////////////
    public static bool SecondSkin = false;
    public static float SkinAmount = 0.5f;

    //////////////////////////////DoomSlayer////////////////////////////////////////
    public static bool DoomSlayer = false;
    public static float DoomBuff = 0.5f;
    public static float DoomHalfDam = 0.7f;
    public static float DoomRecycle = 25f;
    public static float DoomHealth = 10f;
    //////////////////////////////ElephantStrength////////////////////////////////////////
    public static bool ElephantStrength = false;
    public static float EStrength = 0.2f;
    //////////////////////////////AngelsGrace////////////////////////////////////////
    public static bool AngelsGrace = false;

    public static float GracePeriod = 2f;
    //////////////////////////////HungryHippo/////////////////////////////////////
    public static bool HungryHippo = false;
    public static float HippoBuff = 2f;
    //////////////////////////////xXx/////////////////////////////////////
    public static bool xXx = false;
    public static float xXxBuff = 2f;
    //////////////////////////////MetalSkin/////////////////////////////////////
    public static bool MetalSkin = false;
    public static float MetalAmount = 1.5f;
    //////////////////////////////CaptainPlanet/////////////////////////////////////
    public static bool CapPlanet = false;
    public static int CapRecycle = 2;
    //////////////////////////////UNIMPLEMENTED/////////////////////////////////////


    public static Dictionary<string, bool> AugmentationList = new Dictionary<string, bool>()
    {
        {"Epinephrine", Epinephrine}, // passive
        {"BulletTime", BulletTime}, //passive
        {"GunnerGloves", GunnerGloves}, //passive
        {"DeflectionShield", DeflectionShield},
        {"HippoSkin", HippoSkin}, //passive
        {"CasingRecycle", CasingRecycle}, //passive
        {"Whiskers", Whiskers},
        {"HookShot", HookShot},
        {"AutoDoc", AutoDoc}, //passive
        {"DamageBuff", DamageBuff}, //passive
        {"Drone", Drone}, //passive
        {"DroneHead", DroneHead}, //passive
        {"DroneArms", DroneArms}, //passive
        {"DroneBody", DroneBody}, //passive
        {"DroneLegs", DroneLegs}, //passive
        {"Thorns", Thorns}, //passive
        {"CheetahSpeed", CheetahSpeed},
        {"Predator", Predator},
        {"SecondSkin", SecondSkin},
        {"DoomSlayer", DoomSlayer},
        {"ElephantStrength", ElephantStrength},
        {"AngelsGrace",AngelsGrace},
        {"HungryHippo",HungryHippo},
        {"xXx",xXx},
        {"MetalSkin",MetalSkin}

    };

    public static void ResetAugmentations()
    {
        foreach(string key in AugmentationList.Keys.ToList())
        {
            AugmentationList[key] = false;
        }
        Epinephrine = false;
        BulletTime = false;
        GunnerGloves = false;
        DeflectionShield = false;
        HippoSkin = false;
        HippoApplied = false;
        CasingRecycle = false;
        Whiskers = false;
        HookShot = false;
        AutoDoc = false;
        AutoDocUsed = false;
        Drone = false;
        DroneHead = false;
        DroneArms = false;
        DroneBody = false;
        DroneLegs = false;
        Thorns = false;
        CheetahSpeed = false;
        Predator = false;
        SecondSkin = false;
        ElephantStrength = false;
        AngelsGrace = false;
        HungryHippo = false;
        MetalSkin = false;
    }

    public static void PrintDictionary()
    {
        foreach(KeyValuePair<string, bool> aug in AugmentationList)
        {
            Debug.Log(aug.Key + ": " + aug.Value);
        }
    }

}
