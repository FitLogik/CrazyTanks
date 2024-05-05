using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUIController : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] Slider rSlider;
    [SerializeField] Slider gSlider;
    [SerializeField] Slider bSlider;


    private void Start()
    {
        Color _color = PrefsManager.GetPlayerColor(playerNumber);
        SetSliderValues(_color);
    }

    private void SetSliderValues(Color color)
    {
        rSlider.value = color.r;
        gSlider.value = color.g;
        bSlider.value = color.b;
    }

    private void SaveColorChanges()
    {
        Color color = new Color(rSlider.value, gSlider.value, bSlider.value);
        PrefsManager.SetPlayerColor(playerNumber, color);
    }
}
