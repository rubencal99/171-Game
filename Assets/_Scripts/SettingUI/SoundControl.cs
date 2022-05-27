using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeTextUI = null;
    public GameObject Audioplayer;

    private void start(){
        LoadValue();
    }

    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = (volume*100).ToString("");
    }

    public void Savebutton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue",volumeValue);
        LoadValue();
    }

    void LoadValue()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        // AudioListener.volume = volumeValue;
        //Set audio volume to volumeValue
    }


}
