using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _audioManager.PlaySFX(_audioManager.winRound);
    }

    private void PlayerRoundWin(int playerNumber)
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


    public static PlayerController GetPlayer(int playerNumber)
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

        return playerGO.GetComponent<PlayerController>();
    }

}
