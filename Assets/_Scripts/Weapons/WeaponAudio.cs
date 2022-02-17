using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : MonoBehaviour
{

    public FMODUnity.EventReference shootBulletEvent; 
    public FMODUnity.EventReference outOfBulletsEvent;

    public void PlayShootSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(shootBulletEvent, transform.position);
    }

    public void PlayNoBulletsSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(outOfBulletsEvent, transform.position);
    }
}
