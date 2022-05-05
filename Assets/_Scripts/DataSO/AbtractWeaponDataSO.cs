using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/AbstractWeaponData")]
public class AbstractWeaponDataSO : ScriptableObject
{
    [field: SerializeField]
    public Vector3 StartOffset {get; set;} = Vector3.zero;
}
