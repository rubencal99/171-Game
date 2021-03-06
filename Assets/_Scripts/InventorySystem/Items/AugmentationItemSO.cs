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
[CreateAssetMenu(fileName = "New Augmentation Item", menuName = "Inventory System/Items/Augmentation")]
public class AugmentationItemSO : ItemObject
{
    public AugType augType;

    void Awake()
    {
        // initialize as type Augmentation
        type = ItemType.Augmentation;
        stackLimit = 1;
        itemType = Convert.ToInt32(augType);
    }
    void OnValidate()
    {
        // update itemType when augType is changed
        itemType = Convert.ToInt32(augType);
    }
}
