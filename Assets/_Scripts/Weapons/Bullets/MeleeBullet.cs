using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We only need this class to take care of trail rendering
public class MeleeBullet : RegularBullet
{
    float lifetime;

    void Awake(){
        lifetime = BulletData.decayTime;
    }

    public override void Update() {
        if(lifetime < 0.0f)
            Destroy(gameObject);
        
        lifetime -= Time.fixedDeltaTime;
    }

}
