using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will be responsible for direction and target data
public class AIMovementData : MonoBehaviour
{
    [field: SerializeField]
    public Vector3 Direction { get; set; }

    [field: SerializeField]
    public Vector3 PointOfInterest { get; set; }

    [field: SerializeField]
    public Vector3 PointerPosition { get; set; }
}
