using UnityEngine;

public static class PrefsManager
{
    private const string RColorKey = "RColor";
    private const string GColorKey = "GColor";
    private const string BColorKey = "BColor";
    private const string AColorKey = "AColor";

    private const float defaultRColor = 0.129f;
    private const float defaultGColor = 0.4f;
    private const float defaultBColor = 0.129f;
    private const float defaultAColor = 1f;

    public static Color GetPlayerColor(int playerNumber)
    {
        float r = PlayerPrefs.GetFloat(GetPlayerKey(RColorKey, playerNumber), defaultRColor);
        float g = PlayerPrefs.GetFloat(GetPlayerKey(GColorKey, playerNumber), defaultGColor);
        float b = PlayerPrefs.GetFloat(GetPlayerKey(BColorKey, playerNumber), defaultBColor);
        float a = PlayerPrefs.GetFloat(GetPlayerKey(AColorKey, playerNumber), defaultAColor);
        
        Color color = new Color(r, g, b, a);

        return color;
    }

    public static void SetPlayerColor(int playerNumber, Color color)
    {
        PlayerPrefs.SetFloat(GetPlayerKey(RColorKey, playerNumber), color.r);
        PlayerPrefs.SetFloat(GetPlayerKey(GColorKey, playerNumber), color.g);
        PlayerPrefs.SetFloat(GetPlayerKey(BColorKey, playerNumber), color.b);
        PlayerPrefs.SetFloat(GetPlayerKey(AColorKey, playerNumber), color.a);
    }

    private static string GetPlayerKey(string key, int playerNumber)
    {
        return $"{key}_Player{playerNumber}";
    }
}