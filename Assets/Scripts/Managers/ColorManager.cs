using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;

    public static Color ColorP1
    {
        get => Instance._colorP1;
        set => SetColor(1, value);
    }

    public static Color ColorP2
    {
        get => Instance._colorP2;
        set => SetColor(2, value);
    }


    private Color _colorP1;
    private Color _colorP2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _colorP1 = PrefsManager.GetPlayerColor(1);
        _colorP2 = PrefsManager.GetPlayerColor(2);
    }

    public static Color GetColor(int playerNumber)
    {
        if (playerNumber == 1)
        {
            return ColorP1;
        }
        else if (playerNumber == 2)
        {
            return ColorP2;
        }
        else
        {
            Debug.LogError("”казан неверный номер игрока при получении цвета из ColorManager!");
            return Color.green;
        }
    }


    public static void SetColor(int playerNumber, Color color)
    {
        if (playerNumber == 1)
        {
            Instance._colorP1 = color;
        }
        else if (playerNumber == 2)
        {
            Instance._colorP2 = color;
        }
        else
        {
            Debug.LogError("”казан неверный номер игрока при установке цвета в ColorManager!");
        }
    }


    public static void SaveColors()
    {
        PrefsManager.SetPlayerColor(1, ColorP1);
        PrefsManager.SetPlayerColor(2, ColorP2);
    }
}
