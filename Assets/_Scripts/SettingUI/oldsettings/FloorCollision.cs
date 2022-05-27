using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorCollision : MonoBehaviour
{
    [SerializeField] private Slider floorcollideSlider = null;
    [SerializeField] private Text floorcollideTextUI = null;
    public void FloorCollideSlider(float floorcollide)
    {
        floorcollideTextUI.text = (floorcollide*100).ToString();
    }
}
