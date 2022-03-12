using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We only need this class to take care of trail rendering
public class SniperBullet : RegularBullet
{
    TrailRenderer trail;

    void Awake(){
        trail = GetComponentInChildren<TrailRenderer>();
    }

    void OnDestroy(){
        trail.transform.parent = null;
        trail.autodestruct = true;
    }
}
