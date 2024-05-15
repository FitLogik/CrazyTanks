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
    [SerializeField] Sprite healthImage;
    [SerializeField] Sprite freezeImage;



    private void Awake()
    {
        Instance = this;
    }



    public static void SetBonus(int playerID, BonusType bonusType, float lifetime)
    {
        GameObject activeBonusImgGO = Instance.GetBonusGameObect(playerID);

        Image activeBonusImg = activeBonusImgGO.GetComponent<Image>();

        //switch (bonusType)
        //{
        //    case BonusType.Shield:
        //        activeBonusImg.
        //}

        //TODO: Доделать
    }

    private GameObject GetBonusGameObect(int playerID)
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
            throw new ArgumentException("Указан неверный playerID!");
        }
        return gameObject;
    }
}
