using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTextures : MonoBehaviour
{
    [SerializeField] private Slider mapSlider = null;
    [SerializeField] private Text maptextureTextUI = null;
    public void MapSlider(float maptexture)
    {
        maptextureTextUI.text = (maptexture*100).ToString();
    }
}
