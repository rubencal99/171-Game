using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public static float slowDownFactor = 0.05f;
    public static float slowDownLength = 2f;
    public static bool inFreeze = false;
    public static float freezeDuration = 0.1f;
    public static float pendingFreeze = 0;
    public static int freezeQueue = 0;

    public void Awake()
    {
        Instance = this;
    }

    public static void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public static void RevertSlowMotion()
    {
        Time.timeScale = 1;
    }

    public void Freeze()
    {
        // freezeQueue++;
        // pendingFreeze = freezeDuration;
        Debug.Log("In Freeze");
        StartCoroutine(DoFreeze());
    }

    IEnumerator DoFreeze()
    {
        freezeQueue++;
        inFreeze = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(freezeDuration);

        freezeQueue--;
        if(freezeQueue <= 0)
        {
            Time.timeScale = original;
            pendingFreeze = 0;
            inFreeze = false;
        }
    }

    /*public void Update()
    {
        if(pendingFreeze < 0 && !inFreeze)
        {
            StartCoroutine(DoFreeze());
        }
    }*/
}
