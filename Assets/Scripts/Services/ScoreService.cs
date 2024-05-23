using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ScoreService
{
    [DoNotSerialize] public int Player1Score { get; set; }
    [DoNotSerialize] public int Player2Score { get; set; }

    [Range(1f, 100f)] public int MaxScore = 5;

    public bool IsGameEnded
    {
        get
        {
            return Player1Score >= MaxScore || Player2Score >= MaxScore;
        }
    }

    public int? WinningPlayer()
    {
        if (IsGameEnded)
        {
            return Player1Score > Player2Score ? 1 : 2;
        }
        return null;
    }

    public string GetScore(GameType gameType)
    {
        if (gameType == GameType.Game1Player)
        {
            return Player1Score.ToString();
        }
        else if (gameType == GameType.Game2Players)
        {
            return $"{Player1Score}:{Player2Score}";
        }

        return string.Empty;
    }
}