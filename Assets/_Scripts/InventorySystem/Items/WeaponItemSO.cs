using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType{
    Primary,
    Secondary
}
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Items/Weapon")]
public class WeaponItemSO : ItemObject
{
    public WeaponType weaponType;
    void Awake()
    {
        // initialize as type Weapon
        type = ItemType.Weapon;
        stackable = false;
    }
}
