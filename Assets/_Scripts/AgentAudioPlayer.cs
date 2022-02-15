using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: It might be better to seperate this class into individual audio players
// because the required Audio Source component might only play one clip at a time
public class AgentAudioPlayer : MonoBehaviour
{
    public FMODUnity.EventReference damageTakenEvent;
    public FMODUnity.EventReference deathEvent;
    public FMODUnity.EventReference stepEvent;

    public void PlayStepSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(stepEvent, transform.root.position);
    }

    public void PlayDamageSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(damageTakenEvent, transform.root.position);
    }

    public void PlayDeathSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(deathEvent, transform.root.position);
    }
}
