using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Items/Weapon")]
public class WeaponItemSO : ItemObject
{
    void Awake()
    {
        // initialize as type Weapon
        type = ItemType.Weapon;
    }
}
