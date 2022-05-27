using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatedLight : MonoBehaviour
{
    [SerializeField] private Slider instantiatedlightSlider = null;
    [SerializeField] private Text instantiatedlightTextUI = null;
    public void InstantiatedLightSlider(float instantiatedlight)
    {
        instantiatedlightTextUI.text = (instantiatedlight*100).ToString();
    }
}
