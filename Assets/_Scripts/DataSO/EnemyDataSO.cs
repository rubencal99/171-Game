using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField]
    public int MaxHealth { get; set; } = 3;         // Might want to make the set private

    [field: SerializeField]
    public int Damage { get; set; } = 1;         // Might want to make the set private

    [field: SerializeField]
    [Range(1f, 100)]
    public float Range = 10;         // Might want to make the set private
}
