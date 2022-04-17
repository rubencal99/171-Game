using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AugType{
    Head, 
    Body,
    Arm,
    Leg,
    Aux
}
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Items/Augmentation")]
public class AugmentationItemSO : ItemObject
{
    public AugType augType;
    void Awake()
    {
        // initialize as type Augmentation
        type = ItemType.Augmentation;
        stackable = false;
        itemType = Convert.ToInt32(augType);
    }
}

