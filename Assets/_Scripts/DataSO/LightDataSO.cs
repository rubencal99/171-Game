using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Environment/LightData")]
public class LightDataSO : ScriptableObject
{
    [SerializeField]
    public float Intensity = 2f;

    // LightLevel is the multiplier for features like intensity & radius
    [SerializeField]
    public float LightLevel = 1f;
    
    // How much can the Intensity change per frame of animation?
    // Might not need this, this is for a random flicker (like fire)
    // More consistent gameplay would warrant predictable light adjustments
    [SerializeField]
    public float flickerRange = 1f;

    // For now, three different colors:
    // yellow = indoor lights
    // red = indoor lights during chase sequences
    // white = natural lights (moonlight, enemy lights, doesn't change during chase)
    [SerializeField]
    public bool indoor = true;
    [SerializeField]
    public bool chase = false;

    [SerializeField]
    [Range(0, 10)]
    public float innerRadius = 1.5f;
    [SerializeField]
    [Range(0, 15)]
    public float outerRadius = 6f;
}
