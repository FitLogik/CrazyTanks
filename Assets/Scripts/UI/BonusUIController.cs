using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusUIController : MonoBehaviour
{
    public static BonusUIController Instance;

    [Header("UI Objects")]
    [SerializeField] GameObject player1ActiveBonusImage;
    [SerializeField] GameObject player2ActiveBonusImage;

    [Header("Bonus Images")]
    [SerializeField] Sprite shieldImage;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    public static void SetShield(int playerID)
    {
        Image img = Instance.GetPlayerBonusImage(playerID);

        img.gameObject.SetActive(true);
    }

    private Image GetPlayerBonusImage(int playerID)
    {
        GameObject activeBonusImgGO = Instance.GetBonusGameObject(playerID);
        Image bonusImg = activeBonusImgGO.GetComponent<Image>();

        return bonusImg;
    }

    public static void RemoveShield(int playerID)
    {
        Image img = Instance.GetPlayerBonusImage(playerID);

        img.gameObject.SetActive(false);
    }

    private GameObject GetBonusGameObject(int playerID)
    {
        GameObject gameObject;
        if (playerID == 1)
        {
            gameObject = Instance.player1ActiveBonusImage;
        }
        else if (playerID == 2)
        {
            gameObject = Instance.player2ActiveBonusImage;
        }
        else
        {
            throw new ArgumentException("”казан неверный playerID!");
        }
        return gameObject;
    }
}
