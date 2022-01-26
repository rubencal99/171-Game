using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MeleeData")]
public class MeleeDataSO : ScriptableObject
{
    [field: SerializeField]
    [field: Range(1, 50)]
    public int Damage { get; set; } = 10;
}
