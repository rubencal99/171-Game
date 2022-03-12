using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class and deriving classes will tell bullets how to act in game
public abstract class Bullet : MonoBehaviour
{
    [field: SerializeField]
    public virtual BulletDataSO BulletData { get; set; } // Virtual lets us change this 

    public Vector2 direction;
}
