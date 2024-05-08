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
    #endregion

    #region Default Values
    private const float defaultRColor = 0.129f;
    private const float defaultGColor = 0.4f;
    private const float defaultBColor = 0.129f;
    private const float defaultAColor = 1f;

    private const float defaultMusicVolume = 0.5f;
    private const float defaultSFXVolume = 0.5f;
    #endregion

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

        Debug.Log("UpdateMusicVolume");
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

}