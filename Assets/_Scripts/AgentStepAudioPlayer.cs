using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// Inherets from AudioPlayer
public class AgentStepAudioPlayer : AudioPlayer
{
    [SerializeField]
    protected AudioClip stepClip;

    public void PlayStepSound()
    {
        PlayClipWithvariablePitch(stepClip);
    }
}
