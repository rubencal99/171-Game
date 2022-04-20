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
    public static int EpinephrineBoost = 1;
    //////////////////////////////BULLETTIME/////////////////////////////////////
    public static bool BulletTime = false;
    public static float BulletTT = 0.5f;
    public static float BulletTimeIntensity = 0.5f;

    //////////////////////////////GUNNERGLOVE/////////////////////////////////////
    public static bool  GunnerGloves = false;
    public static float GunnerGlovesSpeed = 1.5f;

    //////////////////////////////DEFLECTIONSHIELD////////////////////////////////
    public static bool DeflectionShield = false;
    public static float DefelctionTime = 3f;//this could change

    //////////////////////////////HIPPOSKIN////////////////////////////////////////
    public static bool HippoSkin = false;
    public static bool HippoApplied = false;

    //////////////////////////////CASINGRECYCLE/////////////////////////////////////
    public static bool CasingRecycle = false;
    public static int CasingRecPer = 15; 

    //////////////////////////////WHISKERS//////////////////////////////////////////
    public static bool Whiskers = false;
    public static float whiskersDist = 5f;

    //////////////////////////////HOOKSHOT//////////////////////////////////////////
    public static bool HookShot = false;
    //////////////////////////////AutoDoc/////////////////////////////////////
    public static bool AutoDoc = false;
    public static float AutoDocHeal = 0.05f;
    public static float AutoDocCoolDown = 10f;
    public static bool AutoDocUsed = false;
    //////////////////////////////UNIMPLEMENTED/////////////////////////////////////
    public static bool DamageBuff = false;
    public static float BuffAmount = 0.25f;

    public static bool Drone = false;

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
        {"DamageBuff", DamageBuff} //passive
    };

    public static void ResetAugmentations()
    {
        foreach(string key in AugmentationList.Keys.ToList())
        {
            AugmentationList[key] = false;
        }
    }

    public static void PrintDictionary()
    {
        foreach(KeyValuePair<string, bool> aug in AugmentationList)
        {
            Debug.Log(aug.Key + ": " + aug.Value);
        }
    }

}
