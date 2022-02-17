using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: It might be better to seperate this class into individual audio players
// because the required Audio Source component might only play one clip at a time
public class AgentAudioPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip damageTakenClip;

    [SerializeField]
    protected AudioClip deathClip;

    [SerializeField]
    protected AudioClip stepClip;

    public void PlayStepSound()
    {
        PlayClipWithvariablePitch(stepClip);
    }

    public void PlayDamageSound()
    {
        PlayClipWithvariablePitch(damageTakenClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(deathClip);
    }
}
