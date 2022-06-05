using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Decorations : MonoBehaviour
{
    [SerializeField] private Slider decorSlider = null;
    [SerializeField] private Text decorTextUI = null;
    public void DecorationSlider(float decoration)
    {
        decorTextUI.text = (decoration*100).ToString();
    }
}
