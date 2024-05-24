using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public static Vector2 windDirection = Vector2.right; // Статическое поле для направления ветра
    public static float windStrength = 0f; // Статическое поле для силы ветра

    [Header("Players GameObjects")]
    public GameObject player1;
    public GameObject player2;
    
    [Header("Prefabs")]
    [SerializeField] GameObject Player1WinsCanvas;
    [SerializeField] GameObject Player2WinsCanvas;

    [SerializeField] GameObject WinCanvas;
    public GameObject star0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public int idLevel;
    private int resultLevel;

    AudioManager _audioManager;

    bool isRoundEnded;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;

        GameObject audioManagerGO = GameObject.FindGameObjectWithTag("Audio");
        if (audioManagerGO != null)
        {
            _audioManager = audioManagerGO.GetComponent<AudioManager>();
        }
        else
        {
            Debug.LogError("Не удалось найти объект Audio!");
        }
    }


    private void Start()
    {
        SetBorders();
    }

    private void SetBorders()
    {
        EdgeCollider2D walls = gameObject.GetComponent<EdgeCollider2D>();
        if (walls == null)
        {
            walls = gameObject.AddComponent<EdgeCollider2D>();
        }

        Vector2 borders = GameManager.GetBorders();

        walls.points = new Vector2[]
        {
            new Vector2(-borders.x - 0.33f, -borders.y),
            new Vector2(-borders.x - 0.33f, borders.y * 5), // Умножаем на 5, чтобы стена была высоко 
            new Vector2(borders.x + 0.33f, borders.y * 5),  // и пули не уничтожались при попадании в потолок
            new Vector2(borders.x + 0.33f, -borders.y)
        };
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 windForce = windDirection * windStrength;
            rb.AddForce(windForce);
        }
    }


    public static void SetWindDirection(Vector2 newWindDirection)
    {
        windDirection = newWindDirection;
    }

    public static void SetWindStrength(float newWindStrength)
    {
        windStrength = newWindStrength;
    }


    internal void EndRound(int defeatedPlayerNumber)
    {
        if (!isRoundEnded)
        {
            if (defeatedPlayerNumber == 1)
            {
                PlayerRoundWin(2); // Раунд выиграл 2 игрок
            }
            else if (defeatedPlayerNumber == 2)
            {
                PlayerRoundWin(1); // Раунд выиграл 1 игрок
            }
            Invoke("StartMusic", 0.4f);
        }
        isRoundEnded = true;
    }

    private void StartMusic()
    {
        if (_audioManager != null)
        {
            _audioManager.PlaySFX(_audioManager.winRound);
        }
    }

    private void PlayerRoundWin(int playerNumber)
    {
        if (GameManager.GameType == GameType.Game2Players)
        {
            GameManager.PlayerRoundWin(playerNumber);
            if (playerNumber == 1)
            {
                Player1WinsCanvas.SetActive(true);
            }
            else if (playerNumber == 2)
            {
                Player2WinsCanvas.SetActive(true);
            }
        }
        else if (GameManager.GameType == GameType.Game1PlayerWithBot)
        {
            GameManager.PlayerRoundWin(playerNumber);
            TankBehaviour player = GetPlayer(1);
            if (playerNumber == 1)
            {
                if (player.GetHealth() >= 60)
                {
                    star3.SetActive(true);
                    resultLevel = 3;
                }
                else if (player.GetHealth() >= 30)
                {
                    star2.SetActive(true);
                    resultLevel = 2;
                }
                else if (player.GetHealth() < 30)
                {
                    star1.SetActive(true);
                    resultLevel = 1;
                }
                WinCanvas.SetActive(true);

                if (idLevel == 2)
                {
                    int curResult = PrefsManager.GetLevel2();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel2(resultLevel);
                        if (PrefsManager.GetLevel3() == -1)
                        {
                            PrefsManager.SetLevel3(0);
                        }
                    }
                }
                else if(idLevel == 4)
                {
                    int curResult = PrefsManager.GetLevel4();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel4(resultLevel);
                        if (PrefsManager.GetLevel5() == -1)
                        {
                            PrefsManager.SetLevel5(0);
                        }
                    }
                }
                else if (idLevel == 6)
                {
                    int curResult = PrefsManager.GetLevel6();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel6(resultLevel);
                        if (PrefsManager.GetLevel7() == -1)
                        {
                            PrefsManager.SetLevel7(0);
                        }
                    }
                }
                else if (idLevel == 7)
                {
                    int curResult = PrefsManager.GetLevel7();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel7(resultLevel);
                        if (PrefsManager.GetLevel8() == -1)
                        {
                            PrefsManager.SetLevel8(0);
                        }
                    }
                }
                else if (idLevel == 8)
                {
                    int curResult = PrefsManager.GetLevel8();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel8(resultLevel);
                        if (PrefsManager.GetLevel9() == -1)
                        {
                            PrefsManager.SetLevel9(0);
                        }
                    }
                }
                else if (idLevel == 9)
                {
                    int curResult = PrefsManager.GetLevel9();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel9(resultLevel);
                        if (PrefsManager.GetLevel10() == -1)
                        {
                            PrefsManager.SetLevel10(0);
                        }
                    }
                }
                else if (idLevel == 10)
                {
                    int curResult = PrefsManager.GetLevel10();
                    if (curResult <= resultLevel)
                    {
                        PrefsManager.SetLevel10(resultLevel);
                    }
                }
                else
                {
                    Debug.LogError("Ошибка обнаружения номера уровня!");
                }
            }
            else
            {
                star0.SetActive(true);
                resultLevel = 0;
                WinCanvas.SetActive(true);
            }
        }
    }


    public static TankController GetPlayer(int playerNumber)
    {
        GameObject playerGO;
        if (playerNumber == 1)
        {
            playerGO = instance.player1;
        }
        else if (playerNumber == 2)
        {
            playerGO = instance.player2;
        }
        else
        {
            throw new ArgumentException("Указан неверный playerNumber!");
        }

        return playerGO.GetComponent<TankController>();
    }

}
