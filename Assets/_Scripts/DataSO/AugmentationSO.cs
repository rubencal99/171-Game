using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Augmentation Object", menuName = "Inventory System/Items/Augmentation")]
public class AugmentationSO : ItemObject
{
    public string Name;
    public int Cost;

    public void Awake() {
        Type = ItemType.Augmentation;
    }
}
