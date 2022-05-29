using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{

    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text volumeTextUI = null;
    private FMOD.Studio.Bus Master;
    public float masterVol = 1.0f;

    [SerializeField] private Slider volumeSliderMusic = null;
    [SerializeField] private Text volumeTextMusic = null;
    private FMOD.Studio.Bus Music;
    public float musicVol = 1.0f;

    [SerializeField] private Slider volumeSliderSFX = null;
    [SerializeField] private Text volumeTextSFX = null;
    private FMOD.Studio.Bus SFX;
    public float sfxVol = 1.0f;

    private void Start(){
        LoadValue();
    }

    void Awake(){
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/MUSIC");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");

        // initialize all Vols according to PlayerPrefs if they exist
        if (PlayerPrefs.HasKey("VolumeValue")){
            masterVol = PlayerPrefs.GetFloat("VolumeValue");
        }
        if (PlayerPrefs.HasKey("VolumeValueMusic")){
            musicVol = PlayerPrefs.GetFloat("VolumeValueMusic");
        }
        if (PlayerPrefs.HasKey("VolumeValueSFX")){
            sfxVol = PlayerPrefs.GetFloat("VolumeValueSFX");
        }
    }

    void Update()
    {
        Master.setVolume(masterVol);
        Music.setVolume(musicVol);
        SFX.setVolume(sfxVol);
    }

    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = (volume*100).ToString("");
        masterVol = volume;
    }
    public void VolumeSliderMusic(float volume)
    {
        volumeTextMusic.text = (volume*100).ToString("");
        musicVol = volume;
    }
    public void VolumeSliderSFX(float volume)
    {
        volumeTextSFX.text = (volume*100).ToString("");
        sfxVol = volume;
    }

    public void Savebutton()
    {
        PlayerPrefs.SetFloat("VolumeValue",masterVol);
        PlayerPrefs.SetFloat("VolumeValueMusic", musicVol);
        PlayerPrefs.SetFloat("VolumeValueSFX", sfxVol);
        LoadValue();
    }

    void LoadValue()
    {
        masterVol = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = masterVol;

        musicVol = PlayerPrefs.GetFloat("VolumeValueMusic");
        volumeSliderMusic.value = musicVol;

        sfxVol = PlayerPrefs.GetFloat("VolumeValueSFX");
        volumeSliderSFX.value = sfxVol;
        
        // AudioListener.volume = volumeValue;
        //Set audio volume to volumeValue
    }


}
