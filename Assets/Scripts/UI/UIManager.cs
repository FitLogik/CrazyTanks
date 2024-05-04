using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    [Header("Score")]
    [SerializeField] ScoreUISettings scoreUISettings1Player;
    [SerializeField] ScoreUISettings scoreUISettings2Players;
    [DoNotSerialize] ScoreUISettings scoreUISettingsDefault = new ScoreUISettings();

    public static ScoreUISettings ScoreSettings
    {
        get
        {
            if (GameManager.GameType == GameTypes.Game1Player)
            {
                Debug.Log("Selected 1 player score UI settings");
                return Instance.scoreUISettings1Player;
            }
            else if (GameManager.GameType == GameTypes.Game2Players)
            {
                Debug.Log("Selected 2 players score UI settings");
                return Instance.scoreUISettings2Players;
            }

            Debug.Log("Selected default score UI settings");
            return Instance.scoreUISettingsDefault;
        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


}
