using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerAugmentations : object
{
    [SerializeField]
    public static bool Epinephrine = false;
    public static int EpinephrineBoost = 1;

    public static bool BulletTime = false;
    public static float BulletTT = 0.5f;
    public static float BulletTimeIntensity = 0.5f;

    public static bool  GunnerGloves = false;
    public static float GunnerGlovesSpeed = 1.5f;

    public static bool DeflectionShield = false;

    public static float DefelctionTime = 3f;//this could change

    public static bool HippoSkin = false;

    public static bool HippoApplied = false;

    public static bool CasingRecycle = false;

    public static int CasingRecPer = 100; 

    public static bool Whiskers = false;

    public static Dictionary<string, bool> AugmentationList = new Dictionary<string, bool>()
    {
        {"Epinephrine", Epinephrine},
        {"BulletTime", BulletTime},
        {"GunnerGloves", GunnerGloves},
        {"DeflectionShield", DeflectionShield},
        {"HippoSkin", HippoSkin},
        {"CasingRecycle", CasingRecycle},
        {"Whiskers", Whiskers}
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

    // Start is called before the first frame update
    /*public static void Start()
    {
        Epinephrine = true;
        BulletTime = false;
    }*/
}
