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

    public static bool HookShot = false;

    public static bool HippoSkin = false;

    public static bool hippoApplied = false;

    public static bool Whiskers = false;

    public static bool whiskerApplied = false;

    public static Dictionary<string, bool> AugmentationList = new Dictionary<string, bool>()
    {
        {"Epinephrine", Epinephrine},
        {"BulletTime", BulletTime},
        {"GunnerGloves", GunnerGloves},
        {"HookShot", HookShot},
        {"HippoSkin", HippoSkin}
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
