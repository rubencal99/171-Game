using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory System/Items/Augmentation")]
public class AugmentationItemSO : ItemObject
{
    void Awake()
    {
        // initialize as type Augmentation
        type = ItemType.Augmentation;
    }
}

