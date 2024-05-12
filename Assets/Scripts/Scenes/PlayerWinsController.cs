using System;
using TMPro;
using UnityEngine;

public class PlayerWinsController : MonoBehaviour
{
    [SerializeField] TMP_Text playerWinsText;
    [SerializeField] TankUIController tankUIController;

    private void Start()
    {
        int? winningPlayerNumber = ScoreManager.GetWinningPlayer();
        SetWinningPlayer(winningPlayerNumber);
        SetTankColor(winningPlayerNumber);
    }

    private void SetTankColor(int? winningPlayerNumber)
    {
        if (winningPlayerNumber != null)
        {
            Color color = PrefsManager.GetPlayerColor(winningPlayerNumber.Value);
            tankUIController.SetColor(color);
        }
        else
        {
            Debug.LogError("winningPlayerNumber = null!");
        }
    }

    private void SetWinningPlayer(int? winningPlayerNumber)
    {
        if (winningPlayerNumber != null)
        {
            playerWinsText.text = $"Player {winningPlayerNumber} wins!";
        }
        else
        {
            Debug.LogError("winningPlayerNumber = null!");
        }
    }

    
}
