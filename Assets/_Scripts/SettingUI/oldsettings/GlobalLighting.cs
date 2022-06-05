using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GlobalLighting : MonoBehaviour
{
    [SerializeField] private Slider globallightSlider = null;
    [SerializeField] private Text globallightTextUI = null;

    private void start(){
    LoadValue();
    }
    
    public void GlobalLightSlider(float globallight)
    {
        globallightTextUI.text = (globallight*100).ToString();
    }

   public void Savebutton()
    {
        float globallightValue = globallightSlider.value;
        PlayerPrefs.SetFloat("globallightValue",globallightValue);
        LoadValue();
    }

    void LoadValue()
    {
        float globallightValue = PlayerPrefs.GetFloat("globallightValue");
        globallightSlider.value = globallightValue;
        //global lighting Value change here
    }

}
