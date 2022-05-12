using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolutionsetting : MonoBehaviour
{
    // Update is called once per frame
    public bool full;

    public void fullscr(){
        full = !full;
        Screen.fullScreen = !full;
        Debug.Log("Full Screen: " + (full));
    }

    public void Reso800(){
        Screen.SetResolution(800, 600, Screen.fullScreen);
        Debug.Log("set to 800*600");
    }

    public void Reso720p(){
        Screen.SetResolution(1080, 720, Screen.fullScreen);
        Debug.Log("set to 1080*720");
    }

    public void Reso1080p(){
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        Debug.Log("set to 1920*1080");
    }

}
