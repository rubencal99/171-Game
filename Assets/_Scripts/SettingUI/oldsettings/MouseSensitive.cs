using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitive : MonoBehaviour
{
    [SerializeField] private Slider mousespeedSlider = null;
    [SerializeField] private Text mousespeedTextUI = null;
    public GameObject Cursor;

    private void start(){
        LoadValue();
    }

    public void mouseSlider(float mouseSpeed)
    {
        mousespeedTextUI.text = (mouseSpeed*100).ToString();
    }

    public void Savebutton()
    {
        float mouseValue = mousespeedSlider.value;
        PlayerPrefs.SetFloat("mouseValue",mouseValue);
        LoadValue();
    }

    void LoadValue()
    {
        float mouseValue = PlayerPrefs.GetFloat("mouseValue");
        mousespeedSlider.value = mouseValue;
        //Sensitive Value change here
    }

}
