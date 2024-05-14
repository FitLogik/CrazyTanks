using System;
using TMPro;
using UnityEngine;

public class PlayerWinsController : MonoBehaviour
{
    [SerializeField] TMP_Text playerWinsText;
    [SerializeField] TankUIController tankUIController;

    public GameObject player1Win;
    public GameObject player2Win;

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
            if (winningPlayerNumber == 1)
            {
                player1Win.SetActive(true);
                player2Win.SetActive(false);
            }
            else
            {
                player1Win.SetActive(false);
                player2Win.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("winningPlayerNumber = null!");
        }
    }

    
}
