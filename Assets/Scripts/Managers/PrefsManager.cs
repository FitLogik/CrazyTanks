using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public static class PrefsManager
{
    #region Keys
    private const string RColorKey = "rColor";
    private const string GColorKey = "gColor";
    private const string BColorKey = "bColor";
    private const string AColorKey = "aColor";

    private const string musicKey = "musicVolume";
    private const string SFXKey = "SFXVolume";

    private const string languageKey = "language";

    private const string level1Key = "level1";
    private const string level2Key = "level2";
    private const string level3Key = "level3";
    private const string level4Key = "level4";
    private const string level5Key = "level5";
    private const string level6Key = "level6";
    private const string level7Key = "level7";
    private const string level8Key = "level8";
    private const string level9Key = "level9";
    private const string level10Key = "level10";

    #endregion

    #region Default Values
    private const float defaultRColor = 0.129f;
    private const float defaultGColor = 0.4f;
    private const float defaultBColor = 0.129f;
    private const float defaultAColor = 1f;

    private const float defaultMusicVolume = 0.5f;
    private const float defaultSFXVolume = 0.5f;

    private const int defaultLanguage = 1;

    private const int defaultLevel1 = 0;
    private const int defaultLevel2 = -1;
    private const int defaultLevel3 = -1;
    private const int defaultLevel4 = -1;
    private const int defaultLevel5 = -1;
    private const int defaultLevel6 = -1;
    private const int defaultLevel7 = -1;
    private const int defaultLevel8 = -1;
    private const int defaultLevel9 = -1;
    private const int defaultLevel10 = -1;

    #endregion

    public static Color DefaultColor
    {
        get
        {
            Color color = new Color(defaultRColor,
                                    defaultGColor,
                                    defaultBColor,
                                    defaultAColor);

            return color;
        }
    }

    private static string GetPlayerKey(string key, int playerNumber)
    {
        return $"{key}_Player{playerNumber}";
    }

    #region Color
    public static Color GetPlayerColor(int playerNumber)
    {
        float r = PlayerPrefs.GetFloat(GetPlayerKey(RColorKey, playerNumber), defaultRColor);
        float g = PlayerPrefs.GetFloat(GetPlayerKey(GColorKey, playerNumber), defaultGColor);
        float b = PlayerPrefs.GetFloat(GetPlayerKey(BColorKey, playerNumber), defaultBColor);
        //float a = PlayerPrefs.GetFloat(GetPlayerKey(AColorKey, playerNumber), defaultAColor);

        //Color color = new Color(r, g, b, a);
        Color color = new Color(r, g, b);

        return color;
    }

    public static void SetPlayerColor(int playerNumber, Color color)
    {
        PlayerPrefs.SetFloat(GetPlayerKey(RColorKey, playerNumber), color.r);
        PlayerPrefs.SetFloat(GetPlayerKey(GColorKey, playerNumber), color.g);
        PlayerPrefs.SetFloat(GetPlayerKey(BColorKey, playerNumber), color.b);
        //PlayerPrefs.SetFloat(GetPlayerKey(AColorKey, playerNumber), color.a);
    }
    #endregion

    #region Audio
    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(musicKey, volume);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(musicKey, defaultMusicVolume);
    }

    public static void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFXKey, volume);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFXKey, defaultSFXVolume);
    }
    #endregion

    #region Language
    public static void SetLanguage(int languageID)
    {
        PlayerPrefs.SetInt(languageKey, languageID);
    }

    public static int GetLanguage()
    {
        return PlayerPrefs.GetInt(languageKey, defaultLanguage);
    }
    #endregion

    #region Level
    #region Lvl1
    public static void SetLevel1(int result)
    {
        PlayerPrefs.SetInt(level1Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level2Key, 0);
        }
    }
    public static int GetLevel1()
    {
        return PlayerPrefs.GetInt(level1Key, defaultLevel1);
    }
    #endregion
    #region Lvl2
    public static void SetLevel2(int result)
    {
        PlayerPrefs.SetInt(level2Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level3Key, 0);
        }
    }
    public static int GetLevel2()
    {
        return PlayerPrefs.GetInt(level2Key, defaultLevel2);
    }
    #endregion
    #region Lvl3
    public static void SetLevel3(int result)
    {
        PlayerPrefs.SetInt(level3Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level4Key, 0);
        }
    }
    public static int GetLevel3()
    {
        return PlayerPrefs.GetInt(level3Key, defaultLevel3);
    }
    #endregion
    #region Lvl4
    public static void SetLevel4(int result)
    {
        PlayerPrefs.SetInt(level4Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level5Key, 0);

        }
    }
    public static int GetLevel4()
    {
        return PlayerPrefs.GetInt(level4Key, defaultLevel4);
    }
    #endregion
    #region Lvl5
    public static void SetLevel5(int result)
    {
        PlayerPrefs.SetInt(level5Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level6Key, 0);
        }
    }
    public static int GetLevel5()
    {
        return PlayerPrefs.GetInt(level5Key, defaultLevel5);
    }
    #endregion
    #region Lvl6
    public static void SetLevel6(int result)
    {
        PlayerPrefs.SetInt(level6Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level7Key, 0);
        }
    }
    public static int GetLevel6()
    {
        return PlayerPrefs.GetInt(level6Key, defaultLevel6);
    }
    #endregion
    #region Lvl7
    public static void SetLevel7(int result)
    {
        PlayerPrefs.SetInt(level7Key, result);
        if(result > 0)
        {
            PlayerPrefs.SetInt(level8Key, 0);
        }
    }
    public static int GetLevel7()
    {
        return PlayerPrefs.GetInt(level7Key, defaultLevel7);
    }
    #endregion
    #region Lvl8
    public static void SetLevel8(int result)
    {
        PlayerPrefs.SetInt(level8Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level9Key, 0);
        }
    }
    public static int GetLevel8()
    {
        return PlayerPrefs.GetInt(level8Key, defaultLevel8);
    }
    #endregion
    #region Lvl9
    public static void SetLevel9(int result)
    {
        PlayerPrefs.SetInt(level9Key, result);
        if (result > 0)
        {
            PlayerPrefs.SetInt(level10Key, 0);
        }
    }
    public static int GetLevel9()
    {
        return PlayerPrefs.GetInt(level9Key, defaultLevel9);
    }
    #endregion
    #region Lvl10
    public static void SetLevel10(int result)
    {
        PlayerPrefs.SetInt(level10Key, result);
    }
    public static int GetLevel10()
    {
        return PlayerPrefs.GetInt(level10Key, defaultLevel10);
    }
    #endregion
    #endregion
}