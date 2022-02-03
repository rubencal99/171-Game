using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class contains all the variables that may be buffed / nerfed by passive Liftables
public class PlayerPassives : MonoBehaviour
{
    [SerializeField]
    public float SpeedMultiplier;

    [SerializeField]
    public float ReloadMultiplier;

    [SerializeField]
    public float ROFMultiplier;

    [SerializeField]
    public float BulletCountMultiplier;

    [SerializeField]
    public float DamageMultiplier;

    void Awake(){
        SpeedMultiplier = 1f;
        ReloadMultiplier = 1f;
        ROFMultiplier = 1f;
        BulletCountMultiplier = 1f;
        DamageMultiplier = 1f;
    }
}
