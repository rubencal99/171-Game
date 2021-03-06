using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Augmentation, 
    Weapon,
    Default
}

public abstract class ItemObject : ScriptableObject 
{
    public GameObject prefab;
    public GameObject prefabClone;
    public GameObject pickup;
    public Sprite icon;
    public ItemType type;
    public int itemType;
    [TextArea(15,20)]
    public string description;
    public int stackLimit = 99;
    public string Name;
}