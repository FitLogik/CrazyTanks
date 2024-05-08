using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettings : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] TankUIController tankColorController;
    [SerializeField] Slider rSlider;
    [SerializeField] Slider gSlider;
    [SerializeField] Slider bSlider;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Color color = ColorManager.GetColor(playerNumber);

        UpdateUI(color);
    }

    private void UpdateUI(Color color)
    {
        rSlider.value = color.r;
        gSlider.value = color.g;
        bSlider.value = color.b;

        tankColorController.SetColor(color);
    }

    #region Set colors to the manager
    public void SetColorR(float value)
    {
        Color color = ColorManager.GetColor(playerNumber);
        color.r = value;
        ColorManager.SetColor(playerNumber, color);

        tankColorController.SetColor(color);
    }

    public void SetColorG(float value)
    {
        Color color = ColorManager.GetColor(playerNumber);
        color.g = value;
        ColorManager.SetColor(playerNumber, color);

        tankColorController.SetColor(color);
    }

    public void SetColorB(float value)
    {
        Color color = ColorManager.GetColor(playerNumber);
        color.b = value;
        ColorManager.SetColor(playerNumber, color);

        tankColorController.SetColor(color);
    }
    #endregion
}
