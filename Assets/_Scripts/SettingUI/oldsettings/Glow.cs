using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glow : MonoBehaviour
{
    [SerializeField] private Slider glowSlider = null;
    [SerializeField] private Text glowTextUI = null;
    public void GlowSlider(float glow)
    {
        glowTextUI.text = (glow*100).ToString();
    }
}
