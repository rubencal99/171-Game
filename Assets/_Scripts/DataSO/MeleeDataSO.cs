using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MeleeData")]
public class MeleeDataSO : ScriptableObject
{
    [field: SerializeField]
    [field: Range(0,10)]
    public int MeleeRange {get; set;} = 2;

    [field: SerializeField]
    [field: Range(0,100)]
    public int MeleeSpeed {get; set;} = 5;

    [field: SerializeField]
    [field: Range(0.05f, 2f)]
    public float MeleeDelay { get; set; } = 0.1f;

}
