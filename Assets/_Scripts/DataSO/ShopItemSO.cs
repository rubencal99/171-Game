using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopData")]
public class ShopItemSO : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public int Cost;
}
