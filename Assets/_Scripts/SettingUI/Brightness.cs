using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{

    [SerializeField] private Slider brightSlider = null;
    [SerializeField] private Text brightTextUI = null;
    public Light screenlight;

    private void start(){
        LoadValue();
    }

    public void BrightSlider(float bright)
    {
        brightTextUI.text = (bright*20).ToString("");
    }

    void Update(){
        screenlight.intensity = brightSlider.value;
    }


    public void Savebutton()
    {
        float brightnessValue = brightSlider.value;
        PlayerPrefs.SetFloat("brightnessValue",brightnessValue);
        LoadValue();
    }

    void LoadValue()
    {
        float brightnessValue = PlayerPrefs.GetFloat("brightnessValue");
        brightSlider.value = brightnessValue;
        //brightness Value change here
    }


}

