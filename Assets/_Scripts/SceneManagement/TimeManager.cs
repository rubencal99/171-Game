using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager : object
{
    public static float slowDownFactor = 0.05f;
    public static float slowDownLength = 2f;

    public static void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public static void RevertSlowMotion()
    {
        Time.timeScale = 1;
    }
}
