using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField]
    public BulletDataSO BulletData { get; set; }

    [field: SerializeField]
    [field: Range(0,100)]
    public int MagazineCapacity { get; set; } = 30;

    [field: SerializeField]
    [field: Range(20,400)]
    public int MaxAmmoCapacity { get; set; } = 100;

    /* [field: SerializeField]
    [field: Range(100,400)]
    public int TotalAmmo { get; set; } = 100;*/

    [field: SerializeField]
    public bool AutomaticFire { get; set; } = false;

    [field: SerializeField]
    [field: Range(0.05f, 2f)]
    public float WeaponDelay { get; set; } = 0.1f;
    
    [field: SerializeField]
    [field: Range(0f, 2f)]
    public float WeaponDelayRandomizer { get; set; } = 0.1f;

    [field: SerializeField]
    [field: Range(0.1f, 10)]
    public float ReloadSpeed { get; set; } = 1;

    [field: SerializeField]
    [field: Range(0, 25)]
    public float SpreadAngle { get; set; } = 5;

    [SerializeField]
    private bool multiBulletShot = false;

    [SerializeField]
    private int bulletCount;

    [SerializeField]
    public float recoilIntensity;
    [SerializeField]
    public float recoilFrequency;

    [SerializeField]
    public float recoilTime;

    internal int GetBulletCountToSpawn()
    {
        if (multiBulletShot)
        {
            return bulletCount;
        }
        return 1;
    }
}
