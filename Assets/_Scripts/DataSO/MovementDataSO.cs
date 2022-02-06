using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{   
    [Range(1, 20)]
    public float maxDodgeSpeed = 10;

    [Range(1, 10)]
    public float maxRunSpeed = 5;

    [Range(0.1f, 100)]
    public float acceleration = 50, decceleration = 50;
}
