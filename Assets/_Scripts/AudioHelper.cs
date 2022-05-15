using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public static class AudioHelper 
{
    // Useful FMOD tools

    public static void PlayOneShotWithParam(FMODUnity.EventReference fmodEvent, Vector3 position, params(string name, float value)[] parameters)
    {
        // for 2d sounds, set position to Vector3.zero
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        foreach(var (name, value) in parameters)
        {
            instance.setParameterByName(name, value);
        }

        instance.set3DAttributes(position.To3DAttributes());
        instance.start();
        instance.release();
    }

}
