using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Augmentations/AugmentationData")]
public class AugmentationSO : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public int Cost;
}
