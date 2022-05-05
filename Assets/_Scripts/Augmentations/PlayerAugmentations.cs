using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerAugmentations : object
{
    //need to write backstory for each aug

    [SerializeField]
    //////////////////////////////EPINEPHRINE////////////////////////////////////
    public static bool Epinephrine = false;
<<<<<<< HEAD
    public static int EpinephrineBoost = 1;

=======
    public static float EpinephrineBoost = 1f;
>>>>>>> 373b94f4d48841fc57b0c4e9a8dc9994758341d3
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
    public static float whiskersDist = 5f;
    public static float whiskersDistv2 = 7f;
    public static float whiskersDistv3 = 11f;


    //////////////////////////////HOOKSHOT//////////////////////////////////////////
    public static bool HookShot = false;
    //////////////////////////////AutoDoc/////////////////////////////////////
    public static bool AutoDoc = false;
    public static float AutoDocHeal = 0.05f;
    public static float AutoDocCoolDown = 10f;
    public static bool AutoDocUsed = false;
    //////////////////////////////DamageBuff/////////////////////////////////////
    public static bool DamageBuff = false;
    public static float BuffAmount = 0.25f;
    //////////////////////////////CheetahSpeed/////////////////////////////////////
    public static bool CheetahSpeed = false;
    public static float CSAmount = 0.3f;
    //////////////////////////////Angels Grace/////////////////////////////////////
    public static bool AngelsGrace = false;
    //////////////////////////////Second Skin/////////////////////////////////////
    public static bool SecondSkin = false;
    public static float SkinAmount = 0.5f;

    //////////////////////////////UNIMPLEMENTED/////////////////////////////////////
    public static bool Drone = false;

    public static bool PredatoryInstinct = false;
    public static float PredatoryAmount = 0.5f;

    public static Dictionary<string, bool> AugmentationList = new Dictionary<string, bool>() //head, body, legs, arms, aux
    {
        //head: bulletTime, AngelsGrace, Drone 3
        //Body: HippoSkin, AutoDoc, SecondSkin 3 
        //Arms: Epi, GunnerGloves, CasingRe, DamageBuff 4
        //Legs CheetahSpeed 1
        //Aux: Whiskers, Deflection 2
        {"Epinephrine", Epinephrine}, // passive, arms, "Heals on kill"
        {"BulletTime", BulletTime}, //passive, head, "Slows time"
        {"GunnerGloves", GunnerGloves}, //passive, arms, "Faster reload"
        {"DeflectionShield", DeflectionShield}, // active, aux, "Shield"
        {"HippoSkin", HippoSkin}, //passive, body, "health multiplier"
        {"CasingRecycle", CasingRecycle}, //passive, arms, "save bullets"
        {"Whiskers", Whiskers},// active, aux, "teleport"
        {"HookShot", HookShot},// active, aux , not ready
        {"AutoDoc", AutoDoc}, //passive, body, "regeneration"
        {"DamageBuff", DamageBuff}, //passive, amrs, "Increase damage"
        {"SecondSkin", SecondSkin},//passive, body, "damge negation"
        {"AngelsGrace", AngelsGrace}, //passive, head, "revive"
        {"Drone", Drone},//passive, head, "companion"
        {"CheetahSpeed", CheetahSpeed},// passive, legs, "Move faster"
        {"PredatoryInstinct", PredatoryInstinct}

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
    }

    public static void PrintDictionary()
    {
        foreach(KeyValuePair<string, bool> aug in AugmentationList)
        {
            Debug.Log(aug.Key + ": " + aug.Value);
        }
    }

}
