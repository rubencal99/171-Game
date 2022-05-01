using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Augmentation, 
    Weapon,
    Throwable,
    Default
}

public abstract class ItemObject : ScriptableObject 
{
    public GameObject prefab;
    public GameObject prefabClone;
    public Sprite icon;
    public ItemType type;
    public int itemType;
    [TextArea(15,20)]
    public string description;
    public bool stackable; 
    public string Name;
}