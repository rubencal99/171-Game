using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAugmentations : object
{
    [SerializeField]
    public static bool Epinephrine = false;
    public static int EpinephrineBoost = 40;

    public static bool BulletTime = false;
    public static float BulletTT = 0.5f;
    public static float BulletTimeIntensity = 0.5f;

    // Start is called before the first frame update
    /*public static void Start()
    {
        Epinephrine = true;
        BulletTime = false;
    }*/
}
