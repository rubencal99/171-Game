using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MeleeData")]
public class MeleeDataSO : WeaponDataSO
{

    [field: SerializeField]
    [field: Range(1, 50)]
    public int Damage { get; set; } = 10;

    [field: SerializeField]
    [field: Range(0.01f, 2f)]
    public float RecoveryLength { get; set; } = 1f;

    [field: SerializeField]
    [field: Range(0.01f, 2f)]
    public float ComboWindow { get; set; } = 0.2f;
    [field: SerializeField]
    public int ComboLength { get; set; } = 3;

    [field: SerializeField]
    [field: Range(1, 20)]
    public float KnockbackPower { get; set; } = 5;

    [field: SerializeField]
    [field: Range(0.01f, 1f)]
    public float KnockbackDelay { get; set; } = 0.1f;

}
